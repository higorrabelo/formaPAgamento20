using Solicitacao.Modelos;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

// Classe com método de verificação de pastas do sistema de pagamento
// utilizada para monitorar arquivos de requisição e resposta

namespace Solicitacao {
    public class Monitor {
        private static FileSystemWatcher arquivo;
        private static PrintDialog pd = new PrintDialog();
        private static PrintDocument printDocument = new PrintDocument();
        public static void MonitorarArquivo(string caminho, string filtro) {
            try {
                arquivo = new FileSystemWatcher(caminho, filtro) {
                    IncludeSubdirectories = false
                };     
                        arquivo.Changed += AnalisaArquivo;
            
                
                arquivo.EnableRaisingEvents = true;

                Console.WriteLine("Analise de Arquivo");
            }
            catch(ArgumentException erro) {
                MessageBox.Show("Aqui: "+erro.Message);
            }
        }

        private static void AnalisaCriado(object sender, FileSystemEventArgs ex) {
            MessageBox.Show("Arquivo Criado");

        }

        private static void AnalisaArquivo(object sender, FileSystemEventArgs ex) {
            Pagamento paga = Servicos.abrirArquivo(Application.StartupPath, @"\Requisicao\req.tx3");
            Servicos.EfetuarPagamento(paga);
            Servicos.arquivoResposta(Encoding.ASCII.GetString(paga.getCupomFiscal()), Encoding.ASCII.GetString(paga.getValor()), Servicos.status);
            string local = Application.StartupPath + @"\Resposta\temp.json";
            string conteudo = $"{{\r\n    \"status\":\"true\"\r\n}}";
            Servicos.criarArquivo(conteudo, local);


        }
        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e) {
            Pagamento paga = new Pagamento(112, "0", "0", "0000000", "0000", "Operador", "");
            Servicos.EfetuarPagamento(paga);

           
        }
        private static void PaginaImpressa(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            Font font = new Font("Times New Roman", 11);
            Brush brush = new SolidBrush(Color.Black);
            StreamReader sw = new StreamReader(Application.StartupPath + @"\Comprovantes\comp.txt");
            string conteudo = sw.ReadToEnd();

            e.Graphics.DrawString(conteudo, font, brush, 0, 0);
        }
    }
}
