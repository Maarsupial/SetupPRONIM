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

namespace SetupPRONIM
{
    public partial class Setup : Form
    {
        [DllImport("odbccp32.dll")]
        private static extern bool SQLConfigDataSource(
                IntPtr hwndParent,
                int fRequest,
                string lpszDriver,
                string lpszAttributes
            );

        const int ODBC_ADD_DSN = 1;
        const int ODBC_CONFIG_DSN = 2;
        const int ODBC_REMOVE_DSN = 3;
        const int ODBC_ADD_SYS_DSN = 4;
        const int ODBC_CONFIG_SYS_DSN = 5;
        const int ODBC_REMOVE_SYS_DSN = 6;
        const int ODBC_REMOVE_DEFAULT_DSN = 7;


        [DllImport("odbc32.dll")]
        private static extern short SQLDriverConnect(
                IntPtr ConnectionHandle,
                IntPtr WindowHandle,
                string InConnectionString,
                int StringLength1,
                char[] OutConnectionString,
                int BufferLength,
                int StringLength2Ptr,
                int DriverCompletion
            );

        [DllImport("odbc32.dll")]
        private static extern short SQLAllocHandle(
                short hType,
                IntPtr inputHandle,
                out IntPtr outputHandle
            );

        [DllImport("odbc32.dll")]
        private static extern short SQLSetEnvAttr(
                IntPtr EnvironmentHandle,
                int Attribute,
                IntPtr ValuePtr,
                int StringLength
            );

        [DllImport("odbc32.dll")]
        private static extern short SQLSetConnectAttr(
                IntPtr ConnectionHandle,
                int Attribute,
                IntPtr ValuePtr,
                int StringLength
            );

        [DllImport("odbc32.dll")]
        private static extern short SQLFreeHandle(
                ushort HandleType,
                IntPtr InputHandle
            );

        [DllImport("odbc32.dll")]
        private static extern short SQLDisconnect(
                IntPtr ConnectionHandle
            );

        const short SQL_HANDLE_ENV = 1;
        const short SQL_HANDLE_DBC = 2;
        const short SQL_HANDLE_STMT = 3;
        const short SQL_HANDLE_DESC = 4;
        
        const int SQL_ATTR_ODBC_VERSION = 200;
        const int SQL_OV_ODBC3 = 3;
        const short SQL_SUCCESS = 0;

        const short SQL_NEED_DATA = 99;
        const short DEFAULT_RESULT_SIZE = 1024;
        const string SQL_DRIVER_STR = "DRIVER=SQL SERVER";
        const int SQL_SUCCESS_WITH_INFO = 1;

        const int SQL_LOGIN_TIMEOUT = 103;
        const int SQL_DRIVER_PROMPT = 2;


        private int panelId;
        private string tempStr;

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
                 * 4 Pastas criadas
                 * 4 Arquivos copiados
                 * 1 Pasta temp criada
                 * 8 Instaladores copiados
                 * 8 Instaladores executados
                 * 1 Atualizador executado
                 * X Atalhos selecionados
                 */
                int selectedNodes = 0;
                foreach (TreeNode node in treeViewShortcut.Nodes)
                {
                    if (node.Checked)
                    {
                        selectedNodes++;
                    }
                }
                progressBar.Maximum = 1 + 1 + 4 + 4 + 1 + 8 + 8 + selectedNodes + 1;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;

                backgroundInstaller.RunWorkerAsync();
            }
            else if(panelId == 2)
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

        private void createProcess(string fileName, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = fileName;
            processStartInfo.Arguments = arguments;
            Process process = Process.Start(processStartInfo);
            process.WaitForInputIdle();
            process.WaitForExit();
        }

        private static void directoryCopy(string source, string destination, bool recurse)
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
                    directoryCopy(subdir.FullName, temppath, recurse);
                }
            }
        }

        private void configEnvironment(string server)
        {
        }

        public int index;

        private void backgroundInstaller_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = @"\\PMMCTS01";
            index = 0;

            // Desconecta de qualquer conexão existente com o servidor para evitar erros
            NetworkInterface.disconnectRemote(path);

            // Inicio da configuração do banco de dados

            tempStr = @"Criando SQL Server DSN: " + "PRONIM32";
            this.backgroundInstaller.ReportProgress(index++);

            SQLConfigDataSource(IntPtr.Zero, ODBC_ADD_SYS_DSN, "SQL Server",
                    "DSN=PRONIM32\0" +
                    "Description=PRONIM32\0" +
                    "Server=ADMINBD04\\PRONIM\0" +
                    "Trusted_Connection=No\0"
                );

            tempStr = @"Conectando à ODBC: " + "ADMINBD04";
            this.backgroundInstaller.ReportProgress(index++);

            connectODBC();

            e.Cancel = true;
            backgroundInstaller.ReportProgress(0);
            return;

            // Solicita autenticação para se conectar usuário, a não ser que o usuário cancele a operação
            while(false)
            {
                if (NetworkInterface.connectToRemote(path, null, null, true) == NetworkInterface.getErrorForNumber(NetworkInterface.ERROR_CANCELLED))
                {
                    if (MessageBox.Show("Realmente deseja cancelar a operação?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        backgroundInstaller.ReportProgress(0);
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            path = Path.Combine(path, @"PRONIM");

            tempStr = @"Pasta de instalação: C:\PRONIM\";
            this.backgroundInstaller.ReportProgress(index++);

            // Inicio da configuração do ambiente onde será feita a instalação
            string[] paths = {@"C:\PRONIM\INSTALADORES\",
                    @"C:\PRONIM\ATUALIZADOR\",
                    @"C:\PRONIM\CPNBCASP\2018\",
                    @"C:\ProgramData\PRONIM\SUSINC\"
                };

            foreach (string dir in paths)
            {
                tempStr = @"Criando pasta: " + dir;
                this.backgroundInstaller.ReportProgress(index++);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            string source, destination;
            source = path + @"\CPNBCASP\cpcetil.ini";
            destination = @"C:\PRONIM\CPNBCASP\cpcetil.ini";

            tempStr = @"Copiando arquivo: " + source;
            this.backgroundInstaller.ReportProgress(index++);

            File.Copy(source, destination, true);


            source = path + @"\CPNBCASP\2018\";
            destination = @"C:\PRONIM\CPNBCASP\";

            tempStr = @"Copiando pasta: " + source;
            this.backgroundInstaller.ReportProgress(index++);

            directoryCopy(source, destination, true);


            source = path + @"\ATUALIZADOR\atualizador.exe";
            destination = @"C:\PRONIM\ATUALIZADOR\atualizador.exe";

            tempStr = @"Copiando arquivo: " + source;
            this.backgroundInstaller.ReportProgress(index++);

            File.Copy(source, destination, true);


            source = path + @"\ATUALIZADOR\Configuracao.xml";
            destination = @"C:\ProgramData\PRONIM\SUSINC\Configuracao.xml";

            tempStr = @"Copiando arquivo: " + source;
            this.backgroundInstaller.ReportProgress(index++);

            File.Copy(source, destination, true);

            string tmpFolder = Path.Combine(Application.StartupPath, "temp");

            tempStr = @"Criando pasta: " + tmpFolder;
            this.backgroundInstaller.ReportProgress(index++);

            if (!Directory.Exists(tmpFolder))
            {
                Directory.CreateDirectory(tmpFolder);
            }

            // Instalação de todos os componentes
            string[] files = { "CM_*", "LC_*", "PP_*", "AF_*", "GP_*", "AR_*", "TP_*", "CPInstaladorUnificado_*" };
            foreach (string file in files)
            {
                string tmpPath = path;
                if (file.Equals("CM_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\CM");
                }
                else if (file.Equals("LC_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\LC");
                }
                else if (file.Equals("PP_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\PP");
                }
                else if (file.Equals("AF_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\AF");
                }
                else if (file.Equals("AR_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\AR");
                }
                else if (file.Equals("TP_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\TP");
                }
                else if (file.Equals("CPInstaladorUnificado_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\CPNBCASP");
                }
                else if (file.Equals("GP_*"))
                {
                    tmpPath = Path.Combine(tmpPath, @"INSTALADORES\GP");
                }
                string[] filePath = Directory.GetFiles(tmpPath, file);
                string fileName = Path.GetFileName(filePath[0]);
                string origin = Path.Combine(path, filePath[0]);
                string dest = Path.Combine(tmpFolder, fileName);

                tempStr = "Copiando instalador: " + origin;
                this.backgroundInstaller.ReportProgress(index++);

                // Copia o instalador do servidor para o computador local
                File.Copy(origin, dest, true);

                if (backgroundInstaller.CancellationPending)
                {
                    e.Cancel = true;

                    backgroundInstaller.ReportProgress(0);
                    return;
                }

                tempStr = "Instalando:  " + fileName;
                this.backgroundInstaller.ReportProgress(index++);

                // Executa o instalador com o paramento /S para fazer uma execução "silenciosa"
                createProcess(dest, "/S");

                if (backgroundInstaller.CancellationPending)
                {
                    e.Cancel = true;

                    backgroundInstaller.ReportProgress(0);
                    return;
                }
            }

            // Inicio da criação dos atalhos selecionados
            string[] shortcuts = {"PRONIM CM.lnk",
                "PRONIM LC.lnk",
                "PRONIM PP.lnk",
                "PRONIM AF.lnk",
                "PRONIM GP.lnk",
                "PRONIM CP.lnk",
                "PRONIM AR.lnk",
                "PRONIM TP.lnk"
                };
            foreach (TreeNode node in treeViewShortcut.Nodes)
            {
                if (node.Checked)
                {
                    string shortcut = path + @"\ATUALIZADOR\" + shortcuts[node.Index];
                    string tempFolder = Path.Combine(Application.StartupPath, "temp");
                    string destFolder = Path.Combine(tempFolder, shortcuts[node.Index]);
                    string publicFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

                    tempStr = @"Copiando atalho: " + shortcuts[node.Index];
                    this.backgroundInstaller.ReportProgress(index++);

                    File.Copy(shortcut, destFolder, true);
                    File.Copy(destFolder, Path.Combine(publicFolder, shortcuts[node.Index]), true);
                }
            }

            // Desconecta do servidor
            NetworkInterface.disconnectRemote(path);

            tempStr = "Executando atualizador.exe";
            this.backgroundInstaller.ReportProgress(index++);

            // Executa o atualizador
            createProcess(@"C:\PRONIM\ATUALIZADOR\atualizador.exe", "");

        }

        private void backgroundInstaller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            if(!tempStr.Equals(""))
            {
                //tempStr += " || " + Convert.ToString(index);
                labelProgress.Text = tempStr;
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

        public void connectODBC()
        {
            string[] retval = null;
            string txt = string.Empty;
            IntPtr henv = IntPtr.Zero;
            IntPtr hconn = IntPtr.Zero;
            StringBuilder inString = new StringBuilder(SQL_DRIVER_STR);
            StringBuilder outString = new StringBuilder(DEFAULT_RESULT_SIZE);
            short inStringLength = (short)inString.Length;
            short lenNeeded = 0;

            int retcode;

            try
            {
                if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_ENV, henv, out henv))
                {
                    if(SQL_SUCCESS == SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (IntPtr)SQL_OV_ODBC3, 0))
                    {
                        if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_DBC, henv, out hconn))
                        {
                            SQLSetConnectAttr(hconn, SQL_LOGIN_TIMEOUT, (IntPtr)5, 0);

                            retcode = SQLDriverConnect(
                            hconn,
                            IntPtr.Zero,
                            "DRIVER={SQL Server};DSN={PRONIM32};Server={ADMINBD04\\PRONIM};UID={PRONIMCONSULTA};PWD={#consulta123};Trusted_Connection={No}",
                            "DRIVER={SQL Server};DSN={PRONIM32};Server={ADMINBD04\\PRONIM};UID={PRONIMCONSULTA};PWD={#consulta123};Trusted_Connection={No}".Length,
                            OutConnStr,
                            255,
                            OutConnStrLen,
                            SQL_DRIVER_PROMPT);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Acquire SQL Servier List Error");
            }
            /*
            IntPtr hdbc = IntPtr.Zero;
            IntPtr hstmt = IntPtr.Zero;
            int retcode;

            char[] OutConnStr = new char[255];
            int OutConnStrLen = 0;

            retcode = SQLAllocHandle(SQL_HANDLE_ENV, IntPtr.Zero, henv);

            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)
            {
                retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (IntPtr)SQL_OV_ODBC3, 0);

                if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)
                {
                    retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, hdbc);

                    if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)
                    {
                        SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (IntPtr)5, 0);

                        retcode = SQLDriverConnect(
                            hdbc,
                            IntPtr.Zero,
                            "DRIVER={SQL Server};DSN={PRONIM32};Server={ADMINBD04\\PRONIM};UID={PRONIMCONSULTA};PWD={#consulta123};Trusted_Connection={No}",
                            "DRIVER={SQL Server};DSN={PRONIM32};Server={ADMINBD04\\PRONIM};UID={PRONIMCONSULTA};PWD={#consulta123};Trusted_Connection={No}".Length,
                            OutConnStr,
                            255,
                            OutConnStrLen,
                            SQL_DRIVER_PROMPT);

                        if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)
                        {
                            retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, hstmt);

                            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)
                            {
                                SQLFreeHandle(SQL_HANDLE_STMT, hstmt);
                            }
                            else
                            {
                                MessageBox.Show("OCORREU UM ERRO 5" + Convert.ToString(retcode), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            SQLDisconnect(hdbc);
                        }
                        else
                        {
                            MessageBox.Show("OCORREU UM ERRO 4" + Convert.ToString(retcode), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        SQLFreeHandle(SQL_HANDLE_DBC, hdbc);
                    }
                    else
                    {
                        MessageBox.Show("OCORREU UM ERRO 3" + Convert.ToString(retcode), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("OCORREU UM ERRO 2" + Convert.ToString(retcode), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SQLFreeHandle(SQL_HANDLE_ENV, henv);
            }
            else
            {
                MessageBox.Show("OCORREU UM ERRO 1" + Convert.ToString(retcode), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return retcode;*/
        }
    }
}
