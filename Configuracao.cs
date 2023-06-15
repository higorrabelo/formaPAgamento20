using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solicitacao {
    public partial class Configuracao : Form {
        public Configuracao() {
            InitializeComponent();
            var lista = Servicos.RetornaConfiguração();
            txIP.Text = lista[0][1];
            txLoja.Text = lista[1][1];
            txTerminal.Text = lista[2][1];
            var cnpjs = Servicos.recuperaCNPJ();
            txCliente.Text = cnpjs[0];
            txAuto.Text = cnpjs[1];
        }

        private void button1_Click(object sender, EventArgs e) {
            Servicos.salvarConfiguracao(txIP.Text,txLoja.Text,txTerminal.Text,txCliente.Text,txAuto.Text);
            this.Dispose();
        }
    }
}
