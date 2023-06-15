using Solicitacao.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Solicitacao {
    public partial class TefPagamento : Form {

        private static int retorno;
        private static string mensagem;
        public TefPagamento(string msg) {
            InitializeComponent();
            if (msg.Contains("codigo do supervisor")) {
                txBox.PasswordChar = '*'; 
            }
            else {
                txBox.PasswordChar = '\0';
            }
            txTitulo.Text = msg;
        }

        private void button1_Click(object sender, EventArgs e) {
            retorno = 0;
            mensagem =txBox.Text;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e) {
            retorno = 1;
            mensagem = txBox.Text;
            this.Dispose();
        }

        public static string retornaString() {
            if(retorno == 0) {
                return mensagem;
            }
            else {
                return mensagem;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            
        }
    }
}
