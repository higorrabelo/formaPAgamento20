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
    public class MonitorSistema {
        private static FileSystemWatcher arquivo;
        public static void MonitorarArquivo(string caminho, string filtro) {
            try {
                arquivo = new FileSystemWatcher(caminho, filtro) {
                    IncludeSubdirectories = false
                };
                arquivo.Changed += AnalisaArquivo;


                arquivo.EnableRaisingEvents = true;

                Console.WriteLine("Analise de Arquivo");
            }
            catch (ArgumentException erro) {
                MessageBox.Show("Aqui: " + erro.Message);
            }
        }

        private static void AnalisaArquivo(object sender, FileSystemEventArgs ex) {
            var lista = Servicos.recuperaCNPJ();
            //if (lista[2]=="true"){
           //     Aplicativo.visualizar = true;
            //}
           // else {
               // Aplicativo.visualizar = false;
           // }

        }
  
    }
}
