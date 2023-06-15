using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solicitacao {
    public partial class Operacoes : Form {

        Dictionary<int, string> menu = new Dictionary<int, string>();
        public string mensagem;


        public Operacoes(string msg) {
            InitializeComponent();
            mensagem = msg;
            string[] op = mensagem.TrimEnd(';').Split(';');
            
            //listMenu.Items.Clear();
            
            
            for (int i = 0; i < op.Length; i++) {

                string[] valores = op[i].Split(':');
                
                menu.Add(int.Parse(valores[0]), valores[1]);

                listMenu.Items.Add(menu.ElementAt(menu.Count - 1).Value);
            }
        }

        private void Operacoes_Load(object sender, EventArgs e) {
          
        }

        private void button1_Click(object sender, EventArgs e) {
            if (listMenu.SelectedIndex > -1) {
                mensagem = menu.Keys.ElementAt(listMenu.SelectedIndex).ToString();
                this.Dispose();
            }
            else {
                MessageBox.Show("Selecione uma opção");
            }
        }
        private void _KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                button1_Click(sender, e);
            }
        }

   
    }
}
