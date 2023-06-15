using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicitacao.Modelos {
    public class Pagamento {
        private int funcao;
        private byte[] valor;
        private byte[] cupomFiscal;
        private byte[] dataFiscal;
        private byte[] horaFiscal;
        private byte[] operador;
        private byte[] paramAdicional;

        public Pagamento() { 
        
        }

        public Pagamento(int funcao,string valor, string cupomFiscal, string dataFiscal, string horaFiscal, string operador, string paramAdicional) {
            this.funcao = funcao;
            this.valor = Encoding.ASCII.GetBytes(valor);
            this.cupomFiscal = Encoding.ASCII.GetBytes(cupomFiscal); 
            this.dataFiscal = Encoding.ASCII.GetBytes(dataFiscal);
            this.horaFiscal = Encoding.ASCII.GetBytes(horaFiscal);
            this.operador = Encoding.ASCII.GetBytes(operador);
            this.paramAdicional = Encoding.ASCII.GetBytes(paramAdicional); 
        }
        public int getFuncao() { return funcao;}
        public void setFuncao(int funcao) { this.funcao = funcao;}
        public byte[] getValor() {  return valor;}
        public byte[] getCupomFiscal() {  return cupomFiscal;}
        public byte[] getDataFiscal() { return dataFiscal;}
        public byte[] getHoraFiscal() { return horaFiscal; }
        public byte[] getOperador() { return operador; }
        public byte[] getParameterAdicional() { return paramAdicional; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Função=" + getFuncao()+"\n");
            sb.Append("Valor=" + Encoding.ASCII.GetString(getValor()) + "\n");
            sb.Append("Cupom Fiscal=" + Encoding.ASCII.GetString(getCupomFiscal()) + "\n");
            sb.Append("Data Fiscal=" + Encoding.ASCII.GetString(getDataFiscal())+ "\n");
            sb.Append("Hora Fiscal=" + Encoding.ASCII.GetString(getHoraFiscal())+ "\n");
            sb.Append("Operador="+ Encoding.ASCII.GetString(getOperador())+ "\n");
            sb.Append("Parametros adicionais=" + Encoding.ASCII.GetString(getParameterAdicional())+ "\n");
            return sb.ToString();
        }
    }
}
