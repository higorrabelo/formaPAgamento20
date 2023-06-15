using Solicitacao.Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Solicitacao {
    public partial class Form1 : Form {
        public static EventHandler novo;
        private List<string[]> lista;
        private static DateTime data;
        private static string mes;
        private static string ano;
        private static string horaTratada;
        private static string dia;
        private static string hora;
        private static bool cancelar = false;
        public static bool btn = false;

        public Form1() {
            InitializeComponent();
            lista = Servicos.RetornaConfiguração();
            txParams.Text = lista[3][1];
            txIp.Text = lista[0][1];
            txLoja.Text = lista[1][1];
            txTer.Text = lista[2][1];
            string caminho = Directory.GetCurrentDirectory() + "\\Requisicao\\";
            string filtro = "req.tx3";
            Monitor.MonitorarArquivo(caminho, filtro);
            txComprova.Text = Servicos.retornaMSG();
            txCodigoTransacao.Text = dia+hora;
            novo+=lePinpad;
            resetaConfiguracao();
        }
        public static void lePinpad(object sender, EventArgs e) {
            int i = CliSitef.LeTeclaPinPad();
            if (i != 0) {
                MessageBox.Show("" + i);
            }
        }

        public void codigoCupom() {
            data = DateTime.Now;
            ano = data.ToString("yyyy");
            mes = data.ToString("MM");
            dia = data.ToString("dd");
            hora = data.ToString("HHmmss");
            txCodigoTransacao.Text = ano + mes+ dia + hora;
        }
        private void button1_Click(object sender, EventArgs e) {   
            byte[] ip = Encoding.ASCII.GetBytes(txIp.Text);
            byte[] loja = Encoding.ASCII.GetBytes(txLoja.Text);
            byte[] terminal = Encoding.ASCII.GetBytes(txTer.Text);
            byte[] dados = Encoding.ASCII.GetBytes("");
            int i =  CliSitef.ConfiguraEx(ip,loja,terminal,0,dados);
            string msg = Servicos.CodigoConfiguracao(i);

            if (i != 0) {
                MessageBox.Show("Erro nos Parâmetors de Configuração: "+msg,"Erro na Configuração",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            txCodigoRetorno.Text = "" + i;
            // Parametros da Função(Forma pagamento int, valor byte[], cupom fiscal byte[], data fiscal byte[] AAAAMMDD, horario fiscal byte[] HHMMSS, operador byte[], Parametros Adicioanis )
        }
        public void resetaConfiguracao() {
            
            byte[] ip = Encoding.ASCII.GetBytes(txIp.Text);
            byte[] loja = Encoding.ASCII.GetBytes(txLoja.Text);
            byte[] terminal = Encoding.ASCII.GetBytes(txTer.Text);
            byte[] dados = Encoding.ASCII.GetBytes(txParams.Text);
            int i = CliSitef.ConfiguraEx(ip, loja, terminal, 0,dados );
            string msg = Servicos.CodigoConfiguracao(i);
            if (i != 0) {
                MessageBox.Show("Erro nos Parâmetors de Configuração: " + msg, "Erro na Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            respostasSaida.Text = "";
            txComprova.Text = "";
            txCodigoRetorno.Text = "" + i;
            codigoCupom();
        }
        private void Form1_Load(object sender, EventArgs e) {

            List<Funcoes> func = Servicos.retornaFuncoes();
            foreach(Funcoes funcoes in func) {
                comboFuncoes.Items.Add(funcoes.getCodigo() + ":"+ funcoes.getNome());
            }

            List<string> lista = Impressao.Impressoras();

            comboFuncoes.SelectedIndex = comboFuncoes.FindString("0:");
            foreach(string impressora in lista) {
                txImpessoras.Items.Add(impressora);
            }
        }
        private void button2_Click(object sender, EventArgs e) {
            byte[] msg = Encoding.ASCII.GetBytes(txPin.Text);
            CliSitef.LeSimNaoPinPad(msg);
        }
        private void button3_Click(object sender, EventArgs e) {
            resetaConfiguracao();
            txComprova.Text = "";
            if(txValor.Text==""){
                MessageBox.Show("Insira o Valor da operação", "Valor da Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if(txOperador.Text == "") {
                MessageBox.Show("Adicione o nome do operador", "Nome Operador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                int op = 0;
                Pagamento paga = new Pagamento(op, txValor.Text, txCodigoTransacao.Text, dia, hora, txOperador.Text, "");
                string caminho = Application.StartupPath + "\\Requisicao\\req.tx3";
                Servicos.EfetuarPagamento(paga);
                txComprova.Text = Servicos.retornaMSG();
                List<object> passos = Servicos.retorna();
                foreach (object i in passos) {
                    respostasSaida.Text += i + "\n";
                }
            }
            txComprova.Text = Servicos.retornaMSG();
        }
        private void button5_Click(object sender, EventArgs e) {       
            foreach(string[] s in lista) {
                MessageBox.Show(s[1], s[0].ToUpper());
            }
        }
        private void button6_Click(object sender, EventArgs e) {
            Pagamento paga = new Pagamento(2, "3,000",ano+mes+dia+hora, ano+mes+dia, hora, "Meneslau", "-");
            Servicos.EfetuarPagamento(paga);    
            resetaConfiguracao();
        }
        private void button7_Click(object sender, EventArgs e) {
            int tipoCampo = 0;
            short tamanhoMinimo = 0;
            short tamanhoMaximo = 0;
            byte[] buffer = new byte[20000];
            int codigo = int.Parse(txIDFunc.Text);
            int resultado = CliSitef.ContinuaFuncaoSiTefInterativo(ref codigo, ref tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, buffer, buffer.Length, 0);
            txCodigoRetorno.Text = "" + resultado;
        }

        private void comboFuncoes_SelectedIndexChanged(object sender, EventArgs e) { 
            string[] texto = comboFuncoes.Text.Split(':');
            txIDFunc.Text = "" + texto[0].Trim();
        }

        private void button8_Click(object sender, EventArgs e) {
            CliSitef.EscreveMensagemPinPad(txPin.Text);
        }
        private void button9_Click(object sender, EventArgs e) {
            Servicos.btn(true);
            resetaConfiguracao();

            if (txValor.Text == "") {
                MessageBox.Show("Insira o Valor da operação", "Valor da Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txOperador.Text == "") {
                MessageBox.Show("Adicione o nome do operador", "Nome Operador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                Pagamento paga = new Pagamento(int.Parse(txIDFunc.Text), txValor.Text+",00", txCodigoTransacao.Text, ano+mes+dia, hora, txOperador.Text, "");

               Servicos.EfetuarPagamento(paga);
                txComprova.Text = Servicos.retornaMSG();
                List<object> passos = new List<object>(); 
                passos = Servicos.retorna();
                foreach(object i in passos) {
                    respostasSaida.Text += i + "\n";
                }
            }
            Servicos.salvarArquivo();
        }

        private void button10_Click(object sender, EventArgs e) {
            CliSitef.FechaPinPad();
        }

        private void button11_Click(object sender, EventArgs e) {
            CliSitef.AbrePinPad();
        }
        private void button13_Click(object sender, EventArgs e) {//Botão Imprimir

            using (var pd = new System.Drawing.Printing.PrintDocument()) {
                pd.PrinterSettings.PrinterName = txImpessoras.SelectedItem.ToString();
                pd.PrintPage += Pd_PrintPage;
                pd.Print();
            }
        }

        private void Pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs  e) {
            Font font = new Font("Times New Roman",11);
            Brush brush = new SolidBrush(Color.Black);
            e.Graphics.DrawString(txComprova.Text,font,brush,0,0);
        }
        private void button14_Click(object sender, EventArgs e) {
            cancelar = true;
        }
        public static bool pegaCancelar() {
            return cancelar;   
        }
        private void txComprova_TextChanged(object sender, EventArgs e) {

        }
        private void button12_Click(object sender, EventArgs e) {
            this.Dispose();
        }
        private void txImpessoras_SelectedIndexChanged(object sender, EventArgs e) {
   
        }
        private void button15_Click(object sender, EventArgs e) {
             Servicos.limparMSG();
             txComprova.Text = "";
             respostasSaida.Text = "";
            resetaConfiguracao();
        }
    }
}
