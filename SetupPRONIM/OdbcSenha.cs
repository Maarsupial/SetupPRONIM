using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SetupPRONIM {
    public partial class OdbcSenha : Form {
        public string odbcPswd;

        public OdbcSenha() {
            InitializeComponent();
        }

        private void pswdBox_TextChanged(object sender, EventArgs e) {
            odbcPswd = this.pswdBox.Text;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
            this.Dispose();
        }
    }
}
