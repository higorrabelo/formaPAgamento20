using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicitacao.Modelos {
    public class FormaPagamento {
        private int codigo;
        private string nome;

        public FormaPagamento() { }
        public FormaPagamento(int codigo,string nome) {
            this.codigo = codigo;
            this.nome = nome;
        }
        public int getCodigo() {  return codigo; }
        public string getNome() {  return nome; }
        
        public void setCodigo(int codigo) {  this.codigo = codigo; }
        public void setNome(string nome) { this.nome = nome; }
    }
}
