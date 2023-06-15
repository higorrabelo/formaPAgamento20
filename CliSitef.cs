using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Solicitacao {
    public class CliSitef {

        private static bool _configurado = false;
        private const string _dllFileName = "CliSiTef32I.dll";

        public bool Configurado {
            get { return _configurado; }
        }

        private static byte[] _recebido = new byte[20000];
        public CliSitef() { }

        public enum formaPagamento { Cheque = 1, Debito = 2, Credito = 3 };

        public static int Configura(byte[] pEnderecoIP, byte[] pCodigoLoja, byte[] pNumeroTerminal, short ConfiguraResultado) {
            return ConfiguraIntSiTefInterativo(pEnderecoIP, pCodigoLoja, pNumeroTerminal, ConfiguraResultado);
        }

        public static int ConfiguraEx(byte[] pEnderecoIP, byte[] pCodigoLoja, byte[] pNumeroTerminal, short ConfiguraResultado, byte[] pRestricoes) {
            return ConfiguraIntSiTefInterativoEx(pEnderecoIP, pCodigoLoja, pNumeroTerminal, ConfiguraResultado, pRestricoes);
        }

        public static int ColetaCPF(byte[] pChaveDeAcesso, byte[] pIdentificador, byte[] pEntrada, byte[] pSaida) {
            return ObtemDadoPinPadDiretoEx(pChaveDeAcesso, pIdentificador, pEntrada, pSaida);
        }

        public static int ColetaCPFInterativo(byte[] pChaveDeAcesso, byte[] pIdentificador, byte[] pEntrada) {
            return ObtemDadoPinPadEx(pChaveDeAcesso, pIdentificador, pEntrada);
        }

        public static int Inicia(int Funcao, byte[] pValor, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHorario, byte[] pOperador, byte[] pRestricoes) {
            return IniciaFuncaoSiTefInterativo(Funcao, pValor, pCupomFiscal, pDataFiscal, pHorario, pOperador, pRestricoes);
        }

        public static int Continua(ref int pComando, ref int pTipoCampo, ref short pTamMinimo, ref short pTamMaximo, byte[] pBuffer, int TamBuffer, int Continua) {
            return ContinuaFuncaoSiTefInterativo(ref pComando, ref pTipoCampo, ref pTamMinimo, ref pTamMaximo, pBuffer, TamBuffer, Continua);
        }

        public static void Finaliza(short pConfirma, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHoraFiscal) {
            Finaliza(pConfirma, pCupomFiscal, pDataFiscal, pHoraFiscal);
        }

        public static int LeCartaoDiretoS(byte[] mensagem, byte[] pTipoCampoTrilha1, byte[] pTrilha1, byte[] pTipoCampoTrilha2, byte[] pTrilha2, short pTimeout, short cancelamento) {
            return LeCartaoDiretoSeguro(mensagem, pTipoCampoTrilha1, pTrilha1, pTipoCampoTrilha2, pTrilha2, pTimeout, cancelamento);
        }
        public static void EscreveMensagemPinPad(string mensagem) {
            EscreveMensagemPinPad(Encoding.ASCII.GetBytes(mensagem));
        }

        [DllImport(_dllFileName, EntryPoint = "ConfiguraIntSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ConfiguraIntSiTefInterativo(byte[] pEnderecoIP, byte[] pCodigoLoja, byte[] pNumeroTerminal, short ConfiguraResultado);

        [DllImport(_dllFileName, EntryPoint = "ConfiguraIntSiTefInterativoEx", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ConfiguraIntSiTefInterativoEx(byte[] pEnderecoIP, byte[] pCodigoLoja, byte[] pNumeroTerminal, short ConfiguraResultado, byte[] pRestricoes);

        [DllImport(_dllFileName, EntryPoint = "IniciaFuncaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int IniciaFuncaoSiTefInterativo(int Funcao, byte[] pValor, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHorario, byte[] pOperador, byte[] pRestricoes);

        [DllImport(_dllFileName, EntryPoint = "ContinuaFuncaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ContinuaFuncaoSiTefInterativo(ref int pComando, ref int pTipoCampo, ref short pTamMinimo, ref short pTamMaximo, byte[] pBuffer, int TamBuffer, int Continua);

        [DllImport(_dllFileName, EntryPoint = "FinalizaTransacaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void FinalizaTransacaoSiTefInterativo(short pConfirma, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHoraFiscal);

        [DllImport(_dllFileName, EntryPoint = "EnviaRecebeSiTefDireto", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int EnviaRecebeSiTefDireto(short RedeDestino, short FuncaoSiTef, short OffsetCartao, byte[] pDadosTx, short TamDadosTx, byte[] pDadosRx, short TamMaxDadosRx, short[] pCodigoResposta, short TempoEsperaRx, byte[] pNumeroCupon, byte[] pDataFiscal, byte[] pHorario, byte[] pOperador, short TipoTransacao);

        [DllImport(_dllFileName, EntryPoint = "LeSimNaoPinPad", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LeSimNaoPinPad(byte[] pMsgDisplay);

        [DllImport(_dllFileName, EntryPoint = "EscreveMensagemPinPad", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int EscreveMensagemPinPad(byte[] pMsgDisplay);

        [DllImport(_dllFileName, EntryPoint = "AbrePinPad", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int AbrePinPad();

        [DllImport(_dllFileName, EntryPoint = "FechaPinPad", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FechaPinPad();

        [DllImport(_dllFileName, EntryPoint = "LeCartaoDireto", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LeCartaoDireto(byte[] pMsgDisplay, byte[] trilha1, byte[] trilha2);

        [DllImport(_dllFileName, EntryPoint = "ObtemDadoPinPadDiretoEx", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ObtemDadoPinPadDiretoEx(byte[] pChaveAcesso, byte[] pIdentificador, byte[] pEntrada, byte[] pSaida);

        [DllImport(_dllFileName, EntryPoint = "ObtemDadoPinPadEx", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ObtemDadoPinPadEx(byte[] pChaveAcesso, byte[] pIdentificador, byte[] pEntrada);

        [DllImport(_dllFileName, EntryPoint = "LeCartaoDiretoSeguro", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LeCartaoDiretoSeguro(byte[] mensagem, byte[] pTipoCampoTrilha1, byte[] pTrilha1, byte[] pTipoCampoTrilha2, byte[] pTrilha2, short pTimeout, short cancelamento);

        [DllImport(_dllFileName, EntryPoint = "LeTeclaPinPad", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LeTeclaPinPad();

    }
}
