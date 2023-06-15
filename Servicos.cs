using Solicitacao.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Solicitacao {
    public class Servicos {

        private static string msg;
        private static string msg2;
        private static List<object> passos = new List<object>();
        private static string statusComando = "";
        private static string menu = "";
        private static bool continuidade = false;
        public static bool status;
        private static string codigo;
        private static string valor;
        private static string cancelamento;
        public static bool btnVar;
        private static string nsuHost;
        private static string nsuSitef;
        public static string CodigoConfiguracao(int i) {
            string resposta = "";
            switch (i) {
                case 0: return "Configuração realizada com sucesso";
                case 1: return "Endereço Ip Inválido ou não Resolvido";
                case 2: return "Código da Loja Inválido";
                case 3: return "Código de terminal inválido";
                case 6: return "Erro na inicialização do TCP/IP";
                case 7: return "Falta de Memória";
                case 8: return "Não encontrou a CliSiTef ou ela está com Problemas";
                case 9: return "Configuração de Servidores SiTef foi Excedida";
                case 10: return "Erro de Acesso na Pasta CliSiTef(Possível Falta de Permissão para Escrita)";
                case 11: return "Dados Inválidos passados pela automação";
                case 12: return "Modo Seguro Não Ativo(Possível falta de configuração no servidor SiTef do arquibo .cha)";
                case 13: return "Caminho DLL inválido(o caminho completo das bibliotecas está muito grande)";
                default: return resposta;
            }
        }

        public static string CodigoIniciaSitef(int i) {
            string resposta = "Erros detectados internamente pela rotina.";
            switch (i) {
                case 0: return "Sucesso na Execução da Função";
                case 10000: return "Pode Executar Próxima Rotina";
                case -1: return " Módulo não inicializado. O PDV tentou chamar alguma rotina sem antes executar a função de Configuração.";
                case -2: return "Operação Cancelada Pelo Operador";
                case -3: return "O parâmetro função / modalidade é inexistente/inválido.";
                case -4: return "Falta de memória no PDV.";
                case -5: return "Sem comunicação com o SiTef.";
                case -6: return "Operação cancelada pelo usuário (no pinpad).\r\n";
                case -7: return "Reservado";
                case -8: return "A CliSiTef Desatualizada ou Função sem Implementação";
                case -9: return "A automação chamou a rotina ContinuaFuncaoSiTefInterativo sem antes iniciar uma função interativa";
                case -10: return "Algum parâmetro obrigatório não foi passado pela automação comercial.";
                case -12: return "Erro na execução da rotina iterativa. Provavelmente o processo iterativo anterior não \r\nfoi executado até o final (enquanto o retorno for igual a 10000).\r\n";
                case -13: return " Documento fiscal não encontrado nos registros da CliSiTef. Retornado em funções de \r\nconsulta tais como ObtemQuantidadeTransaçõesPendentes.";
                case -15: return "Operação cancelada pela automação comercial.";
                case -20: return "Parâmetro inválido passado para a função.";
                case -21: return "Utilizada uma palavra proibida, por exemplo SENHA, para coletar dados em aberto no \r\npinpad. Por exemplo na função ObtemDadoPinpadDiretoEx";
                case -25: return "Erro no Correspondente Bancário: Deve realizar sangria.\r\n";
                case -30: return "Erro de acesso ao arquivo. Certifique-se que o usuário que roda a aplicação tem direitos \r\nde leitura/escrita.\r\n";
                case -40: return "Transação negada pelo servidor SiTe";
                case -41: return "Dados Inválidos";
                case -42: return "Reservado";
                case -43: return "Problema na Execução de alguma das Rotinas no pinpad";
                case -50: return "Transação Não Segura";
                case -100: return "Erro Interno no Módulo";
                default: return resposta;
            }

        }

        public static void limparMSG() {
            msg = "";
            msg2 = "";
        }

        public static string Paramateros(string cliente, string automacao) {
            string param = $"[ParmsClient=1={cliente};2={automacao};]";
            return param;
        }
        public static List<Funcoes> retornaFuncoes() {
            List<Funcoes> lista = new List<Funcoes>();
            try {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\funcoes.cfg");
                string linha = sr.ReadLine();
                while (linha != null) {
                    string[] dados = linha.Split(':');
                    lista.Add(new Funcoes(int.Parse(dados[0]), dados[1]));
                    linha = sr.ReadLine();
                }
                sr.Close();
            }
            catch (IOException err) {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lista;
        }
        public static int leitura(string msg) {
            int codigo = 0;
            List<Funcoes> lista = new List<Funcoes>();
            try {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\funcoes.cfg");
                string linha = sr.ReadLine();
                while (linha != null) {
                    string[] dados = linha.Split(':');
                    Funcoes func = new Funcoes(int.Parse(dados[0].Trim()), dados[1].Trim());
                    lista.Add(func);
                    //Console.WriteLine(func.ToString());
                    linha = sr.ReadLine();
                }
                sr.Close();

                return codigo;
            }
            catch (IOException err) {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }

        public static void criarArquivo(string conteudo, string caminho) {

            try {
                StreamWriter sw = new StreamWriter(caminho);
                sw.Write(conteudo.Replace("\r\r", ""));
                sw.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }
        public static void criarLog(string conteudo) {
            DateTime data = DateTime.Now;
            string caminho = Application.StartupPath + "\\Logs\\log.txt";
            try {
                StreamWriter sw = new StreamWriter(caminho, true);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(conteudo);
                sw.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public static Pagamento abrirArquivo(string caminho, string arquivo) {
            // string caminho = Application.StartupPath + "\\Resposta\\resp.tx3";
            List<string> lista = new List<string>();
            try {
                StreamReader sr = new StreamReader(caminho + arquivo);

                string linha = sr.ReadLine();

                while (linha != null) {
                    string[] atributo = linha.Split('=');
                    lista.Add(atributo[1]);
                    linha = sr.ReadLine();
                }
                sr.Close();
                Pagamento paga = new Pagamento(int.Parse(lista[0]), lista[1], lista[2], lista[3], lista[4], lista[5], lista[6]);
                return paga;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        //"aParams:[PortaPinPad=5;2=04988631000111;HabilitaCtls=1]";

        public static List<string[]> RetornaConfiguração() {  //Le arquivo de´configuração no diretório raiz da aplicação
            List<string[]> lista = new List<string[]>();
            StreamReader sr = new StreamReader(Application.StartupPath + "\\config.bra");
            string linha = sr.ReadLine();
            while (linha != null) {
                string[] info = linha.Split(':');
                lista.Add(info);
                linha = sr.ReadLine();
            }
            sr.Close();
            return lista;
        }

        public static void salvarConfiguracao(string ip, string idLoja, string idTerminal, string cnpjCliente, string cnpjAuto) {
            string conteudo = $"servidor:{ip}\r\nidLoja:{idLoja}\r\nidTerminal:{idTerminal}\r\naParms:[ParmsClient=1={cnpjCliente};2={cnpjAuto};]";
            try {
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.bra");
                sw.Write(conteudo);
                sw.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Erro em Salvar ARquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            salvaCNPJ(cnpjCliente, cnpjAuto);
        }

        public static void salvaCNPJ(string cliente, string automacao) {
            string conteudo = $"{cliente}:{automacao}";
            try {
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\id.bra");
                sw.Write(conteudo);
                sw.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Erro em Salvar ARquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<string> recuperaCNPJ() {
            List<string> lista = new List<string>();
            try {
                StreamReader sr = new StreamReader(Application.StartupPath + @"\id.bra");
                string linha = sr.ReadLine();
                string[] separa = linha.Split(':');
                lista.Add(separa[0]);
                lista.Add(separa[1]);
                // lista.Add(separa[2]);
                sr.Close();
            }
            catch (IOException ex) {
                MessageBox.Show(ex.Message, "Erro em Salvar ARquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public static int iniciaTransacao(Pagamento paga) {
            int resposta = CliSitef.IniciaFuncaoSiTefInterativo(paga.getFuncao(), paga.getValor(), paga.getCupomFiscal(), paga.getDataFiscal(), paga.getHoraFiscal(), paga.getOperador(), paga.getParameterAdicional());
            return resposta;
        }

        public static int RotinaResultado(int tipo, byte[] buffer) {
            string mensagem = Encoding.UTF8.GetString(buffer);
            mensagem = mensagem.Substring(0, mensagem.IndexOf('\x0'));
            switch (tipo) {
                case 1:
                    //MessageBox.Show(mensagem.ToString(), "RotinaResultado") ;
                    criarLog("Case 1: " + mensagem.ToString() + "\n");
                    break;

                case 105:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 105 (data e hora da transação no formato aaaaMMddhhmmss)\n\nConteúdo: [" + mensagem + "]");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 110:
                    CliSitef.EscreveMensagemPinPad("110 - Menu Retorno Durante Cancelamento [" + mensagem + "]");
                    // passos.Add("Menu Cancelamento" + tipo);
                    break;

                case 121:
                    //MessageBox.Show("Comprovante Cliente: \n" + mensagem.ToString(), "RotinaResultado");
                    //msg = "";
                    criarLog("Case 121: " + mensagem.ToString() + "\n");
                    // msg = mensagem.ToString();
                    CliSitef.EscreveMensagemPinPad("Imprindo Cupom TEF ");
                    //CliSitef.LeTeclaPinPad();
                    passos.Add("TEF Cliente" + tipo);
                    break;
                case 122:
                    msg = "";
                    criarLog("Case 122: " + mensagem.ToString() + "\n");
                    msg = mensagem.ToString();
                    CliSitef.EscreveMensagemPinPad("Imprimindo via Estabelecimento ");
                    passos.Add("TEF Estabelecimento" + mensagem.ToString());
                    break;

                case 131:
                    //System.Windows.Forms.MessageBox.Show("Rede Destino: [" + mensagem.ToString() + "]", "RotinaResultado");
                    criarLog("Rede Destino:" + "Case 131: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 132:
                    //System.Windows.Forms.MessageBox.Show("Tipo Cartao: [" + mensagem.ToString() + "]", "RotinaResultado");
                    criarLog("132 Tipo Cartao:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("132 Tipo Cartao: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 133:
                    //System.Windows.Forms.MessageBox.Show("Tipo Cartao: [" + mensagem.ToString() + "]", "RotinaResultado");
                    criarLog("133 NSU do SiTef:" + mensagem.ToString() + "\n");
                    nsuSitef = mensagem.ToString();
                    //CliSitef.EscreveMensagemPinPad("133 NSU do SiTef: " + mensagem.ToString() + "\n");
                    passos.Add(tipo + "Tipo Resultado NSU do SiTef: " + mensagem.ToString());
                    break;

                case 134:
                    //System.Windows.Forms.MessageBox.Show("Tipo Cartao: [" + mensagem.ToString() + "]", "RotinaResultado");
                    criarLog("134 NSU do Host:" + mensagem.ToString() + "\n");
                    nsuHost = mensagem.ToString();
                    //CliSitef.EscreveMensagemPinPad("Case 134: " + mensagem.ToString() + "\n");
                    passos.Add(tipo + " Tipo Resultado NSU host:" + "  -  " + mensagem.ToString());
                    break;

                case 135:
                    //System.Windows.Forms.MessageBox.Show("Tipo Cartao: [" + mensagem.ToString() + "]", "RotinaResultado");
                    criarLog("135 Código de Autorização:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("Case 135: Codigo Autorização " + mensagem.ToString() + "\n");
                    passos.Add("Código de Autorização: " + mensagem.ToString());
                    break;

                case 136:
                    //passos.Add("6 Primeiros números do Cartão: " + mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 136 (6 primeiras posições do cartão)\n\nConteúdo:[" + mensagem + "]", "RotinaResultado");
                    break;
                case 153:
                    // ==:> Devolutiva da senha captura no TUICCS Offline
                    criarLog("\n 153 Senha Criptografada:   " + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("153 Senha Criptografada: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    //System.Windows.Forms.MessageBox.Show(mensagem.ToString(), "Senha capturada!");
                    break;

                case 159:
                    criarLog("\n 159 Tipo de MK no Terminal: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("159 Tipo de MK no Terminal: " + mensagem.ToString() + "\n");
                    passos.Add("MK do Terminal: " + tipo);
                    //System.Windows.Forms.MessageBox.Show(ValidaMKTerminal(Convert.ToInt32(mensagem.ToString())), "MK disponivel");
                    break;

                case 160:
                    criarLog(" 160 Cupom Fiscal a ser tratado: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("Case 160: " + mensagem.ToString() + "\n");
                    passos.Add("Cupom Fiscal" + tipo);
                    break;

                case 163:
                    criarLog("163 Data Fiscal: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("163 Data Fiscal: " + mensagem.ToString() + "\n");
                    passos.Add("Data Fiscal: " + tipo);
                    break;

                case 164:
                    criarLog("164 Hora Fiscal: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("164 Hora Fiscal: " + mensagem.ToString() + "\n");
                    passos.Add("Hora Fiscal: " + tipo);
                    break;


                case 170:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 170 (Venda Parcelada Estabelecimento Habilitada)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 171:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 171 (Número mínimo de parcelas - parcelada estabelecimento)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 172:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 172 (Número máximo de parcelas - parcelada estabelecimento)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 173:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 173 (Valor mínimo por parcela - parcelada estabelecimento)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 174:
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo - 174 (Venda parcela administradora habilitada)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 175:
                    // System.Windows.Forms.MessageBox.Show("nTipoCampo - 175 (Número Mínimo de Parcelas - parcelada administradora)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 176:
                    // System.Windows.Forms.MessageBox.Show("nTipoCampo - 176 (Número Máximo de Parcelas - parcelada administradora)\n\nConteúdo: [" + mensagem + "]", "RotinaResultado");
                    break;

                case 200:
                    criarLog("200 Saldo indisponível: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("Case 210: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 210:
                    criarLog("210 Qtd. Transações Pendentes: " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("Case 210: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;
                case 771:
                    criarLog("771 Caregando tabelas: " + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad("Case 210: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    //ExibeMsgPinPad(mensagem.ToString());
                    break;
                case 1319:
                    criarLog("1319 Valor da transação: " + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad("Case 210: " + mensagem.ToString() + "\n");
                    passos.Add("Valor Transação:" + mensagem.ToString());
                    //ExibeMsgPinPad(mensagem.ToString());
                    break;

                case 2421:
                    // System.Windows.Forms.MessageBox.Show("nTipoCampo - 2421 (Função de coleta de dados adicionais do cliente habilitada)\nConteúdo:[" + mensagem + "]", "RotinaResultado");
                    break;

                case 2010:
                    if (mensagem.ToString().Equals("00")) {
                        criarLog(mensagem.ToString());
                        CliSitef.EscreveMensagemPinPad(mensagem.ToString());//aprovada
                        passos.Add("Transação Aprovada");
                        //ExibeMsgPinPad(mensagem.ToString());
                        // System.Windows.Forms.MessageBox.Show("Transação Aprovada!", "Status Transação", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                    else {
                        criarLog(mensagem.ToString());
                        CliSitef.EscreveMensagemPinPad(mensagem.ToString());//Cancelada pelo Usuário
                        passos.Add("Transação Negada");
                        //ExibeMsgPinPad(mensagem.ToString());
                        //System.Windows.Forms.MessageBox.Show(mensagem.ToString(), "Status Transação", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    break;

                case 2090:
                    criarLog(tipo + " - " + mensagem.ToString());
                    //CliSitef.EscreveMensagemPinPad("2090 Tipo de Cartao: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo +"-"+mensagem.ToString());
                    //ExibeMsgPinPad(mensagem.ToString());
                    // System.Windows.Forms.MessageBox.Show("nTipoCampo - 2090 (Tipo do cartão lido)\n\nConteúdo[" + mensagem + "]", "RotinaResultado");
                    break;

                case 2021:
                    criarLog("TipoCampo 2021:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("Case 2021: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2022:
                    criarLog("TipoCampo 2022:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("Case 2022: " + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2023:
                    criarLog("TipoCampo 2023:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("Case 2023: " + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2024:
                    criarLog("TipoCampo 2024:" + mensagem.ToString() + "\n");
                    //CliSitef.EscreveMensagemPinPad("Case 2024: " + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2025:
                    criarLog("TipoCampo 2025:" + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2031:
                    criarLog("TipoCampo 2031:" + mensagem.ToString() + "\n");
                    passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2032:
                    criarLog("TipoCampo 2032:" + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2033:
                    criarLog("TipoCampo 2033:" + mensagem.ToString() + "\n");
                    passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2034:
                    criarLog("TipoCampo 2034:" + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2035:
                    criarLog("TipoCampo 2035:" + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2041:
                    criarLog("TipoCampo 2041:" + mensagem.ToString() + "\n");
                    //passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2042:
                    criarLog("TipoCampo 2042:" + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;
                case 2043:
                    criarLog("TipoCampo 2043:" + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;
                case 2044:
                    criarLog("TipoCampo 2044:" + mensagem.ToString() + "\n");
                    //  passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2045:
                    // criarLog("TipoCampo 2045:" + mensagem.ToString() + "\n");
                    // passos.Add("Tipo Resultado" + tipo);
                    break;

                case 2607:
                    if (mensagem.ToString() == "0010011") {
                        criarLog("2607 \n0010011 - pinblock coletado com chave DES");
                        passos.Add("PinBlock Coletado com chave DES" + tipo);
                        break;
                    }
                    passos.Add("PinBlock Coletado com chave 3DES" + tipo);
                    break;

                case 4125:
                    MessageBox.Show("Reimpresso Comprovante Cliente: \n" + mensagem.ToString(), "RotinaResultado");

                    break;
                case 4126:
                    MessageBox.Show("Reimpresso Comprovante Estabelecimento: \n" + mensagem.ToString(), "RotinaResultado");

                    break;

                default:
                    //ExibeMsgPinPad(mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show("nTipoCampo: [" + tipo.ToString() + "]\nConteudo: [" + mensagem.ToString() + "]", "RotinaResultado");
                    break;
            }

            return tipo;
        }
        public static int RotinaColeta(int comando, int tipoCampo, ref short pTamanhoMinimo, ref short pTamanhoMaximo, byte[] pDadosComando, byte[] pCampo, ref byte[] retornoDadosUsuario) {

            string mensagem = Encoding.UTF8.GetString(pDadosComando);


            mensagem = mensagem.Substring(0, mensagem.IndexOf('\x0'));

            switch (comando) {
                case 0:
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando+"Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 1:
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando+"Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 2:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando + "Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 3:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando + "Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 4:

                    return 0;
                case 11:
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    passos.Add("Comando :" + comando);
                    //System.Windows.Forms.MessageBox.Show(comando + "Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 12:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando + "Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    return 0;
                case 13:
                    return 0;
                case 14:
                    passos.Add($"Comando: {comando} {mensagem.ToString()}");
                    CliSitef.EscreveMensagemPinPad("Bra Solucoes");
                    return 0;
                case 20:
                    passos.Add($"{comando} - {mensagem.ToString()}");
                    var result = System.Windows.Forms.MessageBox.Show($"{tipoCampo} Confirma Sim/Não:" + mensagem.ToString() + "", mensagem.ToString(), System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (result.ToString() == "Yes") {
                        if (mensagem.ToString() == "Conf.reimpressao") {
                            msg = "Reimpressão de Cupom" + "\n" + reimpressaoUltimo();
                            return 0;
                        }
                        if (mensagem.ToString() == "13 - Operacao Cancelada?") {
                            return 1;
                        }
                        //if(mensagem.ToString() == "Sem conexao Servidor") {
                        //  return 1;
                        // }
                    }
                    else {
                        return 1;
                    }
                    break;
                case 21://Menus
                    if (btnVar) {
                        passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                        Operacoes form = new Operacoes(mensagem);
                        form.ShowDialog();
                        retornoDadosUsuario = Encoding.ASCII.GetBytes(String.Format("{0}\0", form.mensagem));
                        return 0;
                    }
                    else {
                        return 0;
                    }
                case 22:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    CliSitef.FechaPinPad();
                    return 0;

                case 23:
                    System.Threading.Thread.Sleep(1000);
                    return 0;
                case 30:
                    //LEITURA DE DADOS COM TAM MIN E MÁX 
                    switch (tipoCampo) {

                        //ENTRADA DE 
                        case 140:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE A DATA DA PRIMEIRA PARCELA NO FORMATO ddmmaaaa"));
                            return 0;
                        //ENTRADA DE SENHA DE ADMINISTRADOR
                        case 500:
                            string senha = campoSenha(mensagem.ToString());
                            if (senha == "12345") {
                                retornoDadosUsuario = Encoding.ASCII.GetBytes(senha);
                                return 0;
                            }
                            else {
                                MessageBox.Show("Senha inválida", "Senha inválida " + tipoCampo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return 1;
                            }

                        //ENTRADA DE NÚMERO DE PARCELAS
                        case 505:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE O NÚMERO DE PARCELAS").Replace(" ", ""));
                            return 0;

                        case 506:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE A DATA DO PRÉ-DATADO NO FORMATO ddMMaaaa"));
                            return 0;

                        //ENTRADA DO NÚMERO DE PARCELAS CDC
                        case 511:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE O NÚMERO DE PARCELAS CDC"));
                            return 0;

                        //ENTRADA DE NÚMERO DO CARTÃO
                        case 512:
                            string texto = EntradaDadosUsuarioStr("DIGITE O NÚMERO DO CARTÃO").Replace(" ", "");
                            passos.Add("\nTamanho do cartão" + texto.Length);
                            if (texto != "") {
                                if (texto.Length <= 19 && texto.Length >= 15) {
                                    retornoDadosUsuario = Encoding.ASCII.GetBytes(texto);
                                    return 0;
                                }
                                else {
                                    MessageBox.Show("Número de Cartão Inválido", "Erro Tamanho no número do cartão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else {
                                MessageBox.Show("Campo Vazio", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return 1;
                        //ENTRADA DE DATA DO VENCIMENTO DO CARTÃO
                        case 513:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE A DATA DE VENCIMENTO DO CARTÃO").Replace(" ", ""));
                            return 0;
                        //ENTRADA DE CÓDIGO DE SEGURANÇA DO CARTÃO
                        case 514:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE O CÓDIGO DE SEGURANÇA DO CARTÃO"));
                            return 0;
                        //ENTRADA DE DATA DE CANCELAMENTO
                        case 515:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE A DATA DA TRANSAÇÃO A SER CANCELADA (DDMMAAAA)"));
                            return 0;
                        case 516:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("FORNECA O NUMERO DO DOCUMENTO A SER CANCELADO"));
                            return 0;
                        case 2370:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr($"Tipo Campo: {tipoCampo}" + mensagem.ToString()));
                            return 0;

                        default:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr(mensagem.ToString()));
                            MessageBox.Show($"Aqui: {tipoCampo}", $"Campo {tipoCampo} ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return 0;

                    }
                //break;
                case 35:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    return 0;
                case 34:

                    switch (tipoCampo) {
                        case 130:
                            return 0;

                        case 146:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("Digite o valor da transacao"));
                            return 0;
                        case 149:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE O VALOR DA PRÉ AUTORIZAÇÃO"));
                            return 0;

                        case 504:
                            retornoDadosUsuario = Encoding.ASCII.GetBytes(EntradaDadosUsuarioStr("DIGITE O VALOR DA TAXA DE SERVIÇO"));
                            return 0;

                    }
                    break;
                case 37:
                    passos.Add("\nComando: " + comando + "-" + mensagem.ToString());
                    CliSitef.EscreveMensagemPinPad(mensagem.ToString());
                    //System.Windows.Forms.MessageBox.Show(comando + "Mensagem Visor: [" + mensagem.ToString() + "]", "RotinaColeta");
                    //break;
                    System.Windows.Forms.MessageBox.Show(comando + "Coleta confirmacao no PinPad: [" + mensagem.ToString() + "]", "RotinaColeta", System.Windows.Forms.MessageBoxButtons.YesNo);
                    return 0;
            }
            retornoDadosUsuario = null;
            return -1;
        }
        public static void EfetuarPagamento(Pagamento paga) {
            int continua;
            int comando = 0;
            int tipoCampo = 0;
            short tamanhoMinimo = 0;
            short tamanhoMaximo = 0;
            byte[] buffer = new byte[20000];
            int retorno = iniciaTransacao(paga);
            while (retorno == 10000) {

                if (!continuidade) {
                    retorno = CliSitef.Continua(ref comando, ref tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, buffer, buffer.Length, 0);
                }
                else {
                    retorno = CliSitef.Continua(ref comando, ref tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, buffer, buffer.Length, -1);
                    status = false;
                }
                if (comando == 0) {
                    continua = RotinaResultado(tipoCampo, buffer);
                }
                else {

                    byte[] aux = null;
                    continua = RotinaColeta(comando, tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, buffer, buffer, ref aux);
                    if (aux != null) {
                        LimparBuffer(buffer);
                        for (int i = 0; i < aux.Length; i++) {
                            buffer[i] = aux[i];
                        }
                    }
                    if (continua == 1) {
                        retorno = CliSitef.Continua(ref comando, ref tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, buffer, buffer.Length, 1);
                    }
                }
            }
            if (retorno == 0) {
                CliSitef.FinalizaTransacaoSiTefInterativo(1, paga.getCupomFiscal(), paga.getDataFiscal(), paga.getHoraFiscal());
                status = true;
            }

            continuidade = false;


        }
        public static void ExibeMsgPinPad(string mensagem) {
            CliSitef.LeSimNaoPinPad(Encoding.ASCII.GetBytes(mensagem));
        }

        public static string retornaMSG() {
            return msg;
        }
        public static string exibeComando() {
            return statusComando;
        }
        public static List<object> retorna() {
            return passos;
        }
        public static void LimparBuffer(byte[] buffer) {
            for (int i = 0; i < buffer.Length; i++) {
                buffer[i] = 0;
            }
        }
        public static string listarMensagens(string mensagem) {
            menu = mensagem;
            string[] lista = mensagem.Split(';');
            string ocorrencias = "";
            for (int i = 0; i < lista.Length; i++) {
                ocorrencias += lista[i] + "\n";
            }
            return ocorrencias;
        }

        public static void salvarArquivo() {
            try {
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Comprovantes\\comp.txt");
                sw.Write(retornaMSG());
                sw.Close();
            }
            catch (IOException ex) {
                MessageBox.Show(ex.Message, "Erro em Abrir Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string reimpressaoUltimo() {
            StreamReader sr = new StreamReader(Application.StartupPath + "\\Comprovantes\\comp.txt");
            string retorno = sr.ReadToEnd();
            sr.Close();
            return retorno;
        }

        public static string EntradaDadosUsuarioStr(string mensagem) {
            return Microsoft.VisualBasic.Interaction.InputBox(mensagem);
        }
        public static string campoSenha(string mensagem) {
            TefPagamento conf = new TefPagamento(mensagem);
            conf.ShowDialog();
            return TefPagamento.retornaString();
        }
        public static void arquivoResposta(string codigo, string valor, bool status) {
            string conteudo = $"{{\r\n   \"operacao\":{{\r\n            \"codigo\":\"{codigo}\",\r\n            \"valor\": \"{valor}\",\r\n            \"status\":{status},\r\n            \"nsuHost\":\"{nsuHost}\",\r\n            \"nsuSitef\":\"{nsuSitef}\"\r\n            }}\r\n}}";
            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\Resposta\resp.json");
            try {

                sw.Write(conteudo);
                sw.Close();
            }
            catch (IOException ex) {
                MessageBox.Show(ex.Message, "Erro em Abrir Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sw.Close();
        }

        public static bool btn(bool btn) {
            btnVar = btn;
            return btnVar;
        }


    }
} 