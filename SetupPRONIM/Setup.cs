using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SetupPRONIM
{
    public partial class Setup : Form
    {
        private int panelId;
        private string tempStr;
        private int progress;
        private const int CM_LC_PP_AF = 1;
        private const int AR_TP = 2;
        private const int CP_GP = 3;
        private bool GP_ODBC = false;

        public Setup()
        {
            InitializeComponent();

            panelId = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (panelId == 0)
            {
                panelShortcut.Visible = true;
                panelStart.Visible = false;
                buttonBack.Visible = true;
                panelId++;
            }
            else if (panelId == 1)
            {
                panelInstall.Visible = true;
                panelShortcut.Visible = false;
                buttonNext.Enabled = false;
                panelId++;

                /*
                 * 1 Criação DSN
                 * 1 Conexão ODBC
                 * 1 Start
                 * 4 Pastas criadas
                 * 4 Arquivos copiados
                 * 1 Atualizador executado
                 * X Atalhos selecionados
                 */
                List<int> install = new List<int>();
                int selectedNodes = 0;
                foreach (TreeNode node in treeViewShortcut.Nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Text.Equals(" CM") || node.Text.Equals(" LC") || node.Text.Equals(" PP") || node.Text.Equals(" AF"))
                        {
                            if (!install.Contains(CM_LC_PP_AF))
                            {
                                install.Add(CM_LC_PP_AF);
                            }
                            selectedNodes++;
                        }
                        else if (node.Text.Equals(" AR") || node.Text.Equals(" TP"))
                        {
                            if (!install.Contains(AR_TP))
                            {
                                install.Add(AR_TP);
                            }
                            selectedNodes++;
                        }
                        else if (node.Text.Equals(" CP"))
                        {
                            if (!install.Contains(CP_GP))
                            {
                                install.Add(CP_GP);
                            }
                            selectedNodes++;
                        }
                        else if (node.Text.Equals(" GP"))
                        {
                            GP_ODBC = true;
                            if (!install.Contains(CP_GP))
                            {
                                install.Add(CP_GP);
                            }
                            selectedNodes++;
                        }
                    }
                }
                foreach (int mode in install)
                {
                    switch (mode)
                    {
                        case CM_LC_PP_AF:
                            selectedNodes += 8;
                            break;
                        case AR_TP:
                            selectedNodes += 4;
                            break;
                        case CP_GP:
                            selectedNodes += 4 + 1 + 1;
                            break;
                    }
                }

                progressBar.Maximum = 1 + 1 + 4 + 4 + 1 + selectedNodes;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;

                backgroundInstaller.RunWorkerAsync();
            }
            else if (panelId == 3)
            {
                this.Close();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (panelId == 1)
            {
                panelStart.Visible = true;
                panelShortcut.Visible = false;
                buttonBack.Visible = false;
                panelId--;
            }
            else if (panelId == 2)
            {
                panelShortcut.Visible = true;
                panelInstall.Visible = false;
                buttonNext.Enabled = true;
                panelId--;
            }
        }

        private void backgroundInstaller_DoWork(object sender, DoWorkEventArgs e)
        {
            string server = @"\\PMMCTS01";
            string workPath = Path.Combine(server, "PRONIM");
            string tempPath = Path.GetTempPath();

            progress = 0;

            // Desconecta de qualquer conexão existente com o servidor para evitar erros
            NetworkInterface.disconnectRemote(server);

            // Configuração do DSN e da conexão ODBC
            reportProgressString(@"Criando SQL Server DSN: PRONIM32");
            ODBCManager.CreateDSN32("PRONIM32", "PRONIM32", @"ADMINBD04\PRONIM", "SQL Server", false, "");

            reportProgressString(@"Conectando à ODBC: ADMINBD04\PRONIM");
            ODBCManager.ConnectODBC("PRONIM32", "SQL Server", @"ADMINBD04\PRONIM", "PRONIMCONSULTA", "#consulta123");

            // Solicita autenticação para se conectar ao servidor
            bool tryAgain = false;
            while (true)
            {
                if (NetworkInterface.connectToRemote(server, null, null, tryAgain) == NetworkInterface.getErrorForNumber(NetworkInterface.ERROR_CANCELLED))
                {
                    if (MessageBox.Show("Realmente deseja cancelar a operação?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        backgroundInstaller.ReportProgress(0);
                        return;
                    }
                    tryAgain = true;
                }
                else break;
            }

            // Configuração do ambiente onde será feita a instalação
            reportProgressString(@"Pasta de instalação: C:\PRONIM\");
            string[] folders = {
                    @"C:\PRONIM\INSTALADORES\",
                    @"C:\PRONIM\ATUALIZADOR\",
                    @"C:\PRONIM\CPNBCASP\2018\",
                    @"C:\ProgramData\PRONIM\SUSINC\"
                };

            foreach (string folder in folders)
            {
                reportProgressString(@"Criando pasta: " + folder);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

            }

            string[] sources = {
                    Path.Combine(workPath, @"CPNBCASP\cpcetil.ini"),
                    Path.Combine(workPath, @"ATUALIZADOR\atualizador.exe"),
                    Path.Combine(workPath, @"ATUALIZADOR\Configuracao.xml"),
                    Path.Combine(workPath, @"CPNBCASP\2018\")
                };
            string[] destinations = {
                    @"C:\PRONIM\CPNBCASP\cpcetil.ini",
                    @"C:\PRONIM\ATUALIZADOR\atualizador.exe",
                    @"C:\ProgramData\PRONIM\SUSINC\Configuracao.xml",
                    @"C:\PRONIM\CPNBCASP\"
                };

            for (int i = 0; i < sources.Length; i++)
            {
                if (i != sources.Length - 1)
                {
                    reportProgressString(@"Copiando arquivo: " + sources[i]);
                    File.Copy(sources[i], destinations[i], true);
                    continue;
                }
                reportProgressString(@"Copiando pasta: " + sources[i]);
                DirectoryCopy(sources[i], destinations[i], true);
            }
            if (GP_ODBC)
            {
                if(Environment.Is64BitOperatingSystem)
                {
                    reportProgressString(@"Copiando arquivo: sqlncli 64 bits.msi");
                    File.Copy(Path.Combine(workPath, @"INSTALADORES\sqlncli 64 bits.msi"), Path.Combine(tempPath, @"sqlncli 64 bits.msi"), true);
                    createProcess(Path.Combine(tempPath, @"sqlncli 64 bits.msi"), "/quiet IACCEPTSQLNCLILICENSETERMS=YES", true);
                }
                else
                {
                    reportProgressString(@"Copiando arquivo: sqlncli 32 bits.msi");
                    File.Copy(Path.Combine(workPath, @"INSTALADORES\sqlncli 32 bits.msi"), Path.Combine(tempPath, @"sqlncli 32 bits.msi"), true);
                    createProcess(Path.Combine(tempPath, @"sqlncli 32 bits.msi"), "/quiet IACCEPTSQLNCLILICENSETERMS=YES", true);
                }
                reportProgressString(@"Criando SQL Server DSN: GP");
                ODBCManager.CreateDSN32("GP", "FOLHA", @"ADMINBD05\PRONIM", "SQL Server Native Client 11.0", false, "");
            }

            // Instalação de todos os componentes necessários
            List<int> install = new List<int>();
            foreach (TreeNode node in treeViewShortcut.Nodes)
            {
                if (node.Checked)
                {
                    if (node.Text.Equals(" CM") || node.Text.Equals(" LC") || node.Text.Equals(" PP") || node.Text.Equals(" AF"))
                    {
                        if (!install.Contains(CM_LC_PP_AF))
                        {
                            install.Add(CM_LC_PP_AF);
                        }
                    }
                    else if (node.Text.Equals(" AR") || node.Text.Equals(" TP"))
                    {
                        if (!install.Contains(AR_TP))
                        {
                            install.Add(AR_TP);
                        }
                    }
                    else if (node.Text.Equals(" CP") || node.Text.Equals(" GP"))
                    {
                        if (!install.Contains(CP_GP))
                        {
                            install.Add(CP_GP);
                        }
                    }
                }
            }

            List<string> setups = new List<string>();
            foreach (int mode in install)
            {
                switch (mode)
                {
                    case CM_LC_PP_AF:
                        setups.Add("CM_*");
                        setups.Add("LC_*");
                        setups.Add("PP_*");
                        setups.Add("AF_*");
                        break;
                    case AR_TP:
                        setups.Add("AR_*");
                        setups.Add("TP_*");
                        break;
                    case CP_GP:
                        setups.Add("CPInstaladorUnificado_*");
                        setups.Add("GP_*");
                        break;
                }
            }

            foreach (string setup in setups)
            {
                string searchIn = workPath;
                switch (setup)
                {
                    case "CM_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\CM");
                        break;
                    case "LC_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\LC");
                        break;
                    case "PP_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\PP");
                        break;
                    case "AF_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\AF");
                        break;
                    case "AR_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\AR");
                        break;
                    case "TP_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\TP");
                        break;
                    case "CPInstaladorUnificado_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\CPNBCASP");
                        break;
                    case "GP_*":
                        searchIn = Path.Combine(searchIn, @"INSTALADORES\GP");
                        break;
                }

                string fileName = Path.GetFileName(Directory.GetFiles(searchIn, setup)[0]);
                string source = Path.Combine(searchIn, fileName);
                string destination = Path.Combine(tempPath, fileName);

                reportProgressString("Copiando instalador: " + fileName);
                Thread.Sleep(50);
                File.Copy(source, destination, true);

                reportProgressString("Instalando:  " + fileName);
                Thread.Sleep(50);
                createProcess(destination, @"/S", true);
            }

            // Criação dos atalhos
            string publicDesktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            foreach (TreeNode node in treeViewShortcut.Nodes)
            {
                if (node.Checked)
                {
                    string shortcutName = "PRONIM" + node.Text + ".lnk";
                    string shortcut = @"ATUALIZADOR\" + shortcutName;
                    shortcut = Path.Combine(workPath, shortcut);

                    reportProgressString(@"Copiando atalho: " + shortcutName);
                    File.Copy(shortcut, Path.Combine(tempPath, shortcutName), true);
                    File.Copy(Path.Combine(tempPath, shortcutName), Path.Combine(publicDesktop, shortcutName), true);
                }
            }

            // Desconecta do servidor
            NetworkInterface.disconnectRemote(server);

            // Executa o atualizador
            reportProgressString("Executando: atualizador.exe");
            createProcess(@"C:\PRONIM\ATUALIZADOR\atualizador.exe", "", true);
        }

        private void backgroundInstaller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            if (!tempStr.Equals(""))
            {
                labelProgress.Text = tempStr;
                labelProgress.Text += " | " + progress.ToString();
                textBoxProgress.AppendText(tempStr + "\r\n");
                textBoxProgress.SelectionStart = textBoxProgress.TextLength;
                textBoxProgress.ScrollToCaret();
            }
            tempStr = "";
        }

        private void backgroundInstaller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                labelProgress.Text = "Instalação cancelada!";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                labelProgress.Text = "Ocorreu um erro durante a instalação!";
            }
            else
            {
                labelProgress.Text = "Tarefa Concluída com sucesso!";
                buttonNext.Text = "Concluir";
                buttonBack.Enabled = false;
                buttonNext.Enabled = true;
                buttonCancel.Enabled = false;
                panelId++;
            }
        }

        private void createProcess(string fileName, string arguments, bool wait)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = fileName;
            processStartInfo.Arguments = arguments;
            Process process = Process.Start(processStartInfo);
            if (wait)
            {
                process.WaitForInputIdle();
                process.WaitForExit();
            }
        }

        private static void DirectoryCopy(string source, string destination, bool recurse)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + source);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destination, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (recurse)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destination, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, recurse);
                }
            }
        }

        private void reportProgressString(string text)
        {
            tempStr = text;
            this.backgroundInstaller.ReportProgress(progress++);
            Thread.Sleep(10);
        }
    }
}
