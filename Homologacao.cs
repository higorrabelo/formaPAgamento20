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

namespace Solicitacao {
    public partial class Homologacao : Form {
        private static DateTime data = DateTime.Now;
        private static string mes = (data.Month < 10) ? ("0" + data.Month) : "" + data.Month;
        private static string horaTratada = (data.Hour < 10) ? "0" + data.Hour : data.Hour + "";
        private static string dia = data.Year + "" + mes + "" + data.Day;
        private static string hora = horaTratada + "" + data.Minute + "" + data.Second;
        private static int op;
        private static string menu;

        private static EventHandler evento;

        public Homologacao() {
            InitializeComponent();
            txIP.Text = "127.0.0.1";
            txUnidade.Text = "00000000";
            txTerminal.Text = "SD123456";
            txCliente.Text = "12345123000133";
            txAutomacao.Text = "19155197000166";
            txOperador.Text = "Operador";
            txCodigo.Text = dia + hora;
            op = 0;
        }
        private void button17_Click(object sender, EventArgs e) {
            byte[] ip = Encoding.UTF8.GetBytes(txIP.Text);
            byte[] idLoja = Encoding.UTF8.GetBytes(txUnidade.Text);
            byte[] terminal = Encoding.UTF8.GetBytes(txTerminal.Text);
            byte[] param = Encoding.UTF8.GetBytes(Servicos.Paramateros(txCliente.Text,txAutomacao.Text));

            int i = CliSitef.ConfiguraEx(ip, idLoja, terminal,0,param);

            if (i != 0) {
                string msg = Servicos.CodigoConfiguracao(i);
                MessageBox.Show(msg,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show("Configurado com Sucesso");
            }

        }

        private void button18_Click(object sender, EventArgs e) {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Dispose();
        }

        private void Homologacao_Load(object sender, EventArgs e) {
            

            
        }

        private void button21_Click(object sender, EventArgs e) {
            op = 3;
            alteraBt();
        }

        private void button20_Click(object sender, EventArgs e) {
            op = 2;
            alteraBt();
        }

        private void button22_Click(object sender, EventArgs e) {
            op = 0;
            alteraBt();
        }

        private void button19_Click(object sender, EventArgs e) {
          
        }
        public void alteraBt() {
            if (op == 0) {
                btPaga.Text = "Realiza Pagamento : MENU";
            }
            else if (op == 2) {
                btPaga.Text = "Realiza Pagamento : Debito";
            }
            else {
                btPaga.Text = "Realiza Pagamento : Credito";
            }
        }

        private void button17_Click_1(object sender, EventArgs e) {
            byte[] ip = Encoding.UTF8.GetBytes(txIP.Text);
            byte[] idLoja = Encoding.UTF8.GetBytes(txUnidade.Text);
            byte[] terminal = Encoding.UTF8.GetBytes(txTerminal.Text);
            byte[] param = Encoding.UTF8.GetBytes(Servicos.Paramateros(txCliente.Text, txAutomacao.Text));

            int i = CliSitef.ConfiguraEx(ip, idLoja, terminal, 0, param);

            if (i != 0) {
                string msg = Servicos.CodigoConfiguracao(i);
                MessageBox.Show(msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show("Configurado com Sucesso");
            }
        }

        private void button18_Click_1(object sender, EventArgs e) {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Dispose();
        }

        private void btPaga_Click(object sender, EventArgs e) {
            Pagamento pagar = new Pagamento(op, txValor.Text, txCodigo.Text, dia, hora, txOperador.Text, "");
            //MessageBox.Show("Aqui");
            Servicos.EfetuarPagamento(pagar);
            txCodigo.Text = dia + hora;
        }

        private void button20_Click_1(object sender, EventArgs e) {
            op = 2;
            alteraBt();
        }

        private void button21_Click_1(object sender, EventArgs e) {
            op = 3;
            alteraBt();
        }

        private void button22_Click_1(object sender, EventArgs e) {
            op = 0;
            alteraBt();
        }
       
    }
}
