using Solicitacao.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solicitacao {
    public partial class Aplicativo : Form {
        private static DateTime data;
        private static string mes;
        private static string ano;
        private static string dia;
        private static string hora;
        private PrintDialog pd = new PrintDialog();
        private PrintDocument printDocument = new PrintDocument();
        public static bool visualizar;
        
        public Aplicativo() {
            InitializeComponent();
            string caminho = Directory.GetCurrentDirectory() + "\\Requisicao\\";
            string filtro = "req.tx3";
            Monitor.MonitorarArquivo(caminho, filtro);
           // MonitorSistema.MonitorarArquivo(Application.StartupPath, "id.bra");
            resetaConfiguracao();
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void configuraçãoTotemToolStripMenuItem_Click(object sender, EventArgs e) {
            Configuracao config = new Configuracao();
            config.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e) {
            var lista = Servicos.RetornaConfiguração();
            var cnpjs = Servicos.recuperaCNPJ();
            byte[] ip = Encoding.ASCII.GetBytes(lista[0][1]);
            byte[] loja = Encoding.ASCII.GetBytes(lista[1][1]);
            byte[] terminal = Encoding.ASCII.GetBytes(lista[2][1]);
            byte[] dados = Encoding.ASCII.GetBytes($"[ParmsClient=1={cnpjs[0]};2={cnpjs[1]};]");
            int i = CliSitef.ConfiguraEx(ip, loja, terminal, 0, dados);

            string msg = Servicos.CodigoConfiguracao(i);

            if (i != 0) {
                MessageBox.Show("Erro nos Parâmetors de Configuração: " + msg, "Erro na Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Pagamento paga = new Pagamento(110,"0","0","0000000","0000","Operador","");
            Servicos.EfetuarPagamento(paga);
        }

        private void button2_Click(object sender, EventArgs e) {
            Servicos.btn(true);
            string valor = Servicos.EntradaDadosUsuarioStr("Digite o Valor do Pagamento");
            Pagamento paga = new Pagamento(0, valor, codigoCupom(), ano+mes+dia, hora, "Operador", "");
            Servicos.EfetuarPagamento(paga);
            Servicos.salvarArquivo();

            if (Servicos.status) {
                printDocument.PrintPage += PaginaImpressa;
                if (pd.ShowDialog() == DialogResult.OK) printDocument.Print();
                Servicos.arquivoResposta(Encoding.ASCII.GetString(paga.getCupomFiscal()), Encoding.ASCII.GetString(paga.getValor()), Servicos.status);
            }
            Servicos.arquivoResposta(Encoding.ASCII.GetString(paga.getCupomFiscal()), Encoding.ASCII.GetString(paga.getValor()), Servicos.status);

        }
        public string codigoCupom() {
            data = DateTime.Now;
            ano = data.ToString("yyyy");
            mes = data.ToString("MM");
            dia = data.ToString("dd");
            hora = data.ToString("HHmmss");
            return ano + mes + dia + hora;
        }

        public void resetaConfiguracao() {

            var lista = Servicos.RetornaConfiguração();
            var cnpjs = Servicos.recuperaCNPJ();
            byte[] ip = Encoding.ASCII.GetBytes(lista[0][1]);
            byte[] loja = Encoding.ASCII.GetBytes(lista[1][1]);
            byte[] terminal = Encoding.ASCII.GetBytes(lista[2][1]);
            byte[] dados = Encoding.ASCII.GetBytes($"[ParmsClient=1={cnpjs[0]};2={cnpjs[1]};]");
            int i = CliSitef.ConfiguraEx(ip, loja, terminal, 0, dados);

            string msg = Servicos.CodigoConfiguracao(i);

            if (i != 0) {
                MessageBox.Show("Erro nos Parâmetors de Configuração: " + msg, "Erro na Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            codigoCupom();
        }

        private void avançadoToolStripMenuItem_Click(object sender, EventArgs e) {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e) {
            Pagamento paga = new Pagamento(112, "0", "0", "0000000", "0000", "Operador", "");
            Servicos.EfetuarPagamento(paga);         
            printDocument.PrintPage += PaginaImpressa;
            if (pd.ShowDialog() == DialogResult.OK) printDocument.Print();
        }
        private void PaginaImpressa(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            Font font = new Font("Times New Roman", 11);
            Brush brush = new SolidBrush(Color.Black);
            StreamReader sw = new StreamReader(Application.StartupPath + @"\Comprovantes\comp.txt");
            string conteudo = sw.ReadToEnd();
            
            e.Graphics.DrawString(conteudo, font, brush, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e) {
            Pagamento paga = new Pagamento(200, "0", "0", "0000000", "0000", "Operador", "");
            Servicos.EfetuarPagamento(paga);
        }

        private void button4_Click(object sender, EventArgs e) {
            Configuracao config = new Configuracao();
            config.ShowDialog();
        }

        public void mostra() {
            if (visualizar) {
                Opacity = 100;
            }
            else {
                Opacity = 0;
            }
        }

        private void Aplicativo_Load(object sender, EventArgs e) {
          
        }
    }
}
