using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicitacao.Modelos {
    public class Funcoes {
        private int codigo;
        private string nome;

        public Funcoes() {

        }
        public Funcoes(int codigo, string nome) {
            this.codigo = codigo;
            this.nome = nome;
        }

        public int getCodigo() {
            return codigo;
        }
        public string getNome() {
            return nome;
        }
        public void setCodigo(int codigo) {
            this.codigo = codigo;
        }
        public void setNome(string nome) {
            this.nome = nome;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Codigo: "+getCodigo()+"\n");
            sb.Append("Nome: "+getNome()+"\nf ");
            return sb.ToString();
        }

    }
}
