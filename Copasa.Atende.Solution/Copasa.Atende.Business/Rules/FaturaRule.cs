using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using QRCoder;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Fatura.
    /// </summary>
    public class FaturaRule : BaseRule, IFaturaRule
    {
        private IISSCN6ISFDRepository _iSSCN6ISFDRepository;
        private IISSCN6ISFPRepository _iSSCN6ISFPRepository;
        private IISSCN6ISDFRepository _iSSCN6ISDFRepository;
        private IISSCN6ISSVRepository _iSSCN6ISSVRepository;
        private IISSCN6ISGPRepository _iSSCN6ISGPRepository;
        private IISSCN6ISPDRepository _iSSCN6ISPDRepository;
        private IISSCN6ISCBRepository _iSSCN6ISCBRepository;
        private IISSCN6ISNFRepository _iSSCN6ISNFRepository;
        private IISSCN6ISTPRepository _iSSCN6ISTPRepository;
        private IISSCN6ISCFRepository _iSSCN6ISCFRepository;
        private IBrokerSCN6EFEMRepository _brokerSCN6EFEMRepository;
        private ICodigoBarrasRule _codigoBarrasRule;
        private IMensagemRepository _mensagemRepository;
        private ILog _log;

        /// <summary>
        /// Construtor Fatura.
        /// </summary>
        /// <param name="iSSCN6ISFDRepository">IISSCN6ISFDRepository.</param>
        /// <param name="iSSCN6ISFPRepository">IISSCN6ISFPRepository.</param>
        /// <param name="iSSCN6ISDFRepository">IISSCN6ISDFRepository.</param>
        /// <param name="iSSCN6ISSVRepository">IISSCN6ISSVRepository.</param>
        /// <param name="iSSCN6ISGPRepository">IISSCN6ISGPRepository.</param>
        /// <param name="iSSCN6ISPDRepository">IISSCN6ISPDRepository.</param>
        /// <param name="iSSCN6ISCBRepository">IISSCN6ISCBRepository.</param>
        /// <param name="iSSCN6ISNFRepository">IISSCN6ISNFRepository.</param>
        /// <param name="iSSCN6ISTPRepository">IISSCN6ISTPRepository.</param>
        /// <param name="iSSCN6ISCFRepository">IISSCN6ISCFRepository.</param>
        /// <param name="brokerSCN6EFEMRepository">IBrokerSCN6EFEMRepository.</param>
        /// <param name="codigoBarrasRule">ICodigoBarrasRule.</param>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="log">ILog.</param>
        public FaturaRule(
            IISSCN6ISFDRepository iSSCN6ISFDRepository,
            IISSCN6ISFPRepository iSSCN6ISFPRepository,
            IISSCN6ISDFRepository iSSCN6ISDFRepository,
            IISSCN6ISSVRepository iSSCN6ISSVRepository,
            IISSCN6ISGPRepository iSSCN6ISGPRepository,
            IISSCN6ISPDRepository iSSCN6ISPDRepository,
            IISSCN6ISCBRepository iSSCN6ISCBRepository,
            IISSCN6ISNFRepository iSSCN6ISNFRepository,
            IISSCN6ISTPRepository iSSCN6ISTPRepository,
            IISSCN6ISCFRepository iSSCN6ISCFRepository,
            IBrokerSCN6EFEMRepository brokerSCN6EFEMRepository,
            ICodigoBarrasRule codigoBarrasRule,
            IMensagemRepository mensagemRepository,
            ILog log
            )
        {
            _iSSCN6ISFDRepository = iSSCN6ISFDRepository;
            _iSSCN6ISFPRepository = iSSCN6ISFPRepository;
            _iSSCN6ISDFRepository = iSSCN6ISDFRepository;
            _iSSCN6ISSVRepository = iSSCN6ISSVRepository;
            _iSSCN6ISGPRepository = iSSCN6ISGPRepository;
            _iSSCN6ISPDRepository = iSSCN6ISPDRepository;
            _iSSCN6ISCBRepository = iSSCN6ISCBRepository;
            _iSSCN6ISNFRepository = iSSCN6ISNFRepository;
            _iSSCN6ISTPRepository = iSSCN6ISTPRepository;
            _iSSCN6ISCFRepository = iSSCN6ISCFRepository;
            _brokerSCN6EFEMRepository = brokerSCN6EFEMRepository;
            _codigoBarrasRule = codigoBarrasRule;
            _mensagemRepository = mensagemRepository;
            _log = log;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista faturas em débito.
        /// </summary>
        public BaseResponse SCN6ISFD(SCN6ISFDSend sCN6ISFDSend, bool pesquisaTodosDados)
        {
            SCN6ISFDReceive sCN6ISFDReceive = (SCN6ISFDReceive)_iSSCN6ISFDRepository.Connect(sCN6ISFDSend);
            
            if (sCN6ISFDReceive.IsValid)
            {
                foreach (SCN6ISFDReceiveFaturas fatura in sCN6ISFDReceive.faturas)
                {
                    if (!"0".Equals(fatura.referencia))
                    {
                        if (pesquisaTodosDados)
                        {
                            
                            SCN6ISCBSend sCN6ISCBSend = new SCN6ISCBSend();
                            sCN6ISCBSend.numeroFatura = fatura.numeroFatura;
                            SCN6ISCBReceive sCN6ISCBReceive = (SCN6ISCBReceive)_iSSCN6ISCBRepository.Connect(sCN6ISCBSend);
                            if (sCN6ISCBReceive.IsValid)
                            {
                                fatura.numeroCodigoBarras = sCN6ISCBReceive.codigoBarrasFormatado;
                                fatura.numeroCodigoBarrasFormatado = sCN6ISCBReceive.codigoBarras;
                            }
                        }
                    }
                }
            }
            
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISFDReceive;
            return retorno;
        }

        /// <summary>
        /// Lista faturas pagas.
        /// </summary>
        public BaseResponse SCN6ISFP(SCN6ISFPSend sCN6ISFPSend)
        {
            SCN6ISFPReceive sCN6ISFPReceive = (SCN6ISFPReceive)_iSSCN6ISFPRepository.Connect(sCN6ISFPSend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISFPReceive;
            return retorno;
        }

        /// <summary>
        /// Tarifa proporcional.
        /// </summary>
        public BaseResponse SCN6ISTP(SCN6ISTPSend sCN6ISTPSend)
        {
            SCN6ISTPReceive sCN6ISTPReceive = (SCN6ISTPReceive)_iSSCN6ISTPRepository.Connect(sCN6ISTPSend);
            if (!"".Equals(sCN6ISTPReceive.descricaoRetorno))
                sCN6ISTPReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISTPReceive.descricaoRetorno);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISTPReceive;
            return retorno;
        }

        /// <summary>
        /// Detalhe fatura.
        /// </summary>
        public BaseResponse SCN6ISDF(SCN6ISDFSend sCN6ISDFSend)
        {
            SCN6ISDFReceive sCN6ISDFReceive;
            if ("undefined".Equals(sCN6ISDFSend.anoMesReferencia))
            {
                sCN6ISDFReceive = new SCN6ISDFReceive();
                sCN6ISDFReceive.descricaoRetorno = "Informe a referência corretamente";
            }
            else
            {
                sCN6ISDFReceive = (SCN6ISDFReceive)_iSSCN6ISDFRepository.Connect(sCN6ISDFSend);
                if (!"".Equals(sCN6ISDFReceive.codigoRetornoSicom) && !"0".Equals(sCN6ISDFReceive.codigoRetornoSicom))
                {
                    sCN6ISDFReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISDFReceive.codigoRetornoSicom);
                }
            }
            sCN6ISDFReceive.valorPago = sCN6ISDFReceive.valor;
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISDFReceive;
            return retorno;
        }

        /// <summary>
        /// Gera parcelamento de débito.
        /// </summary>
        public BaseResponse SCN6ISGP(SCN6ISGPSend sCN6ISGPSend)
        {
            SCN6ISGPReceive sCN6ISGPReceive = (SCN6ISGPReceive)_iSSCN6ISGPRepository.Connect(sCN6ISGPSend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISGPReceive;
            return retorno;
        }

        /// <summary>
        /// Consulta parcelamento de débito.
        /// </summary>
        public BaseResponse SCN6ISPD(SCN6ISPDSend sCN6ISPDSend)
        {
            SCN6ISPDReceive sCN6ISPDReceive = (SCN6ISPDReceive)_iSSCN6ISPDRepository.Connect(sCN6ISPDSend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISPDReceive;
            return retorno;
        }

        /// <summary>
        /// Nota fiscal fatura.
        /// </summary>
        public BaseResponse SCN6ISNF(SCN6ISNFSend sCN6ISNFSend)
        {
            SCN6ISNFReceive sCN6ISNFReceive = (SCN6ISNFReceive)_iSSCN6ISNFRepository.Connect(sCN6ISNFSend);
            if (!"".Equals(sCN6ISNFReceive.codigoRetornoSicom) && !"0".Equals(sCN6ISNFReceive.codigoRetornoSicom))
            {
                sCN6ISNFReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISNFReceive.codigoRetornoSicom);
            }

            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISNFReceive;
            return retorno;
        }

        /// <summary>
        /// Simula fatura.
        /// </summary>
        public BaseResponse SCN6ISCF(SCN6ISCFSend sCN6ISCFSend)
        {
            SCN6ISCFReceive sCN6ISCFReceive = (SCN6ISCFReceive)_iSSCN6ISCFRepository.Connect(sCN6ISCFSend);
            if (!"".Equals(sCN6ISCFReceive.codigoRetornoSicom) && !"0".Equals(sCN6ISCFReceive.codigoRetornoSicom))
            {
                sCN6ISCFReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISCFReceive.codigoRetornoSicom);
            }
            _log.AddLog("  Retorno SCN6ISCF:" + sCN6ISCFReceive.ToString().Replace(";", "\r\n    "));

            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISCFReceive;
            return retorno;
        }

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        public WebResponse retornaFaturaPDF(string numFatura)
        {
            int quantidadeZeros = 14 - numFatura.Trim().Length;
            for (int i = 0; i < quantidadeZeros; i++)
                numFatura = "0" + numFatura;
            string strUri = ConfigurationManager.AppSettings["UrlServicoSegundaViaFatura"].ToString();
            Uri uri = new Uri(strUri + numFatura);
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            return response;
        }

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        public BaseResponse retornaFaturaPDF(SCN6EFEMSend sCN6EFEMSend)
        {
            BaseResponse retorno = _brokerSCN6EFEMRepository.Connect(sCN6EFEMSend,_log);
            /*
            int quantidadeZeros = 14 - numFatura.Trim().Length;
            for (int i = 0; i < quantidadeZeros; i++)
                numFatura = "0" + numFatura;
            string strUri = ConfigurationManager.AppSettings["UrlServicoSegundaViaFatura"].ToString();
            Uri uri = new Uri(strUri + numFatura);
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            Stream retorno = response.GetResponseStream();
            return retorno;
            */
            return retorno;

        }

        /// <summary>
        /// Retorna imagem do código de barras.
        /// </summary>
        public Stream retornaCodigoBarras(string codigo)
        {
            return _codigoBarrasRule.GerarImagemEmMemoria(codigo);
        }

        private string formataValor(string valor)
        {
            if (valor == null)
                valor = "";
            valor = valor.Trim();
            string tamanho = "";
            if (valor.Length == 0)
                tamanho = "00";
            else if (valor.Length > 9)
                tamanho = valor.Length.ToString();
            else
                tamanho = "0"+ valor.Length.ToString();
            return tamanho + valor;
        }

        /// <summary>
        /// Retorna imagem de QRCode.
        /// </summary>
        public Stream retornaQRCode(string numeroFatura,string valorFatura)
        {
            StringBuilder texto = new StringBuilder();
            string gui = "br.gov.bcb.pix";
            string chaveEnderecamento = "17281106000103";
            string merchantName = "Copasa";
            string merchantCity = "Belo Horizonte";
            string transactionAmount = valorFatura;
            transactionAmount = transactionAmount.Replace("*", "");
            transactionAmount = transactionAmount.Replace("R", "");
            transactionAmount = transactionAmount.Replace("$", "");
            transactionAmount = transactionAmount.Replace(".", "");
            transactionAmount = transactionAmount.Replace(",", ".");

            string transactionIdentification;
            string numeroConvenio = "077911";
            string somatorioNumeroConvenio = "25";
            numeroFatura = numeroFatura.Replace(".", "");
            numeroFatura = numeroFatura.Replace("-", "");
            transactionIdentification = "REC" + numeroConvenio + somatorioNumeroConvenio + numeroFatura;

            texto.Append("000201");
            texto.Append("010211");

            int tamanho26 = gui.Length + chaveEnderecamento.Length;
            texto.Append("26" + tamanho26.ToString());
            texto.Append("00" + formataValor(gui));
            texto.Append("01" + formataValor(chaveEnderecamento));
            texto.Append("52040000");
            texto.Append("5303986");
            texto.Append("54" + formataValor(transactionAmount));
            texto.Append("5802BR");
            texto.Append("59"+formataValor(merchantName));
            texto.Append("60"+ formataValor(merchantCity));
            int tamanho62 = transactionIdentification.Length + 4;
            texto.Append("6202"+ tamanho62);
            texto.Append("05"+formataValor(transactionIdentification));

            //string texto = "00020101021126440014br.gov.bcb.pix0122fulano2019@example.com5204000053039865802BR5913FULANO DE TAL6008BRASILIA6304";
            //string texto2 = "503002080000024400003886030400000000010100";

            /*
            var retorno = Crc16Ccitt(Encoding.ASCII.GetBytes(texto2)).ToString("X");
            var CRC16 = GenCrc16(Encoding.ASCII.GetBytes(texto), texto2.Length).ToString("X");
            string input = texto2;
            var bytes = HexToBytes(input);
            Crc16Rule crc16Rule = new Crc16Rule();
            string hex = crc16Rule.ComputeChecksum(bytes).ToString("x2");
            var retorno4 = CRCForModbus(texto);
            */
            string textoGerado = texto.ToString();
            string CRC16 = GenCrc16(Encoding.ASCII.GetBytes(textoGerado), textoGerado.Length).ToString("X"); ;


            QRCodeGenerator qrGenerator = new QRCodeGenerator();


            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textoGerado + CRC16,
            QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            MemoryStream memoryStream = new MemoryStream();
            qrCodeImage.Save(memoryStream, ImageFormat.Jpeg);
            long posicao = memoryStream.Position;
            memoryStream.Position = 0;
            return memoryStream;

        }

        private string CalcCRC16(string strInput)
        {
            ushort crc = 0x0000;
            byte[] data = GetBytesFromHexString(strInput);
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)(data[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x8005);
                    else
                        crc <<= 1;
                }
            }
            return crc.ToString("X4");
        }

        private Byte[] GetBytesFromHexString(string strInput)
        {
            Byte[] bytArOutput = new Byte[] { };
            if (!string.IsNullOrEmpty(strInput) && strInput.Length % 2 == 0)
            {
                SoapHexBinary hexBinary = null;
                try
                {
                    hexBinary = SoapHexBinary.Parse(strInput);
                    if (hexBinary != null)
                    {
                        bytArOutput = hexBinary.Value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bytArOutput;
        }

        private ushort Crc16Ccitt(byte[] bytes)
        {
            const ushort poly = 0x1021;
            ushort[] table = new ushort[256];
            ushort initialValue = 0x0000;
            ushort temp, a;
            ushort crc = initialValue;
            for (int i = 0; i < table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort)((temp << 1) ^ poly);
                    else
                        temp <<= 1;
                    a <<= 1;
                }
                table[i] = temp;
            }
            for (int i = 0; i < bytes.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xff & bytes[i]))]);
            }
            return crc;
        }

        private ushort GenCrc16(byte[] c, int nByte)
        {
            ushort Polynominal = 0x1021;
            ushort InitValue = 0x0;

            ushort i, j, index = 0;
            ushort CRC = InitValue;
            ushort Remainder, tmp, short_c;
            for (i = 0; i < nByte; i++)
            {
                short_c = (ushort)(0x00ff & (ushort)c[index]);
                tmp = (ushort)((CRC >> 8) ^ short_c);
                Remainder = (ushort)(tmp << 8);
                for (j = 0; j < 8; j++)
                {

                    if ((Remainder & 0x8000) != 0)
                    {
                        Remainder = (ushort)((Remainder << 1) ^ Polynominal);
                    }
                    else
                    {
                        Remainder = (ushort)(Remainder << 1);
                    }
                }
                CRC = (ushort)((CRC << 8) ^ Remainder);
                index++;
            }
            return CRC;
        }


        // CRC-CCITT (0xFFFF) with poly 0x1021
        // input (hex string) =  "503002080000024400003886030400000000010100"
        // result expected (hex string) = "354E"
        private string CalcCRC162(string strInput)
        {
            ushort temp = 0;
            ushort crc = 0xFFFF;
            byte[] bytes = GetBytesFromHexString2(strInput);
            for (int j = 0; j < bytes.Length; j++)
            {
                crc = (ushort)(crc ^ bytes[j]);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) == 1)
                        crc = (ushort)((crc >> 1) ^ 0x1021);
                    else
                        crc >>= 1;
                }
            }
            crc = (ushort)~(uint)crc;
            temp = crc;
            crc = (ushort)((crc << 8) | (temp >> 8 & 0xFF));
            return crc.ToString("X4");
        }

        private Byte[] GetBytesFromHexString2(string strInput)
        {
            Byte[] bytArOutput = new Byte[] { };
            if (!string.IsNullOrEmpty(strInput) && strInput.Length % 2 == 0)
            {
                SoapHexBinary hexBinary = null;
                try
                {
                    hexBinary = SoapHexBinary.Parse(strInput);
                    if (hexBinary != null)
                        bytArOutput = hexBinary.Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bytArOutput;
        }

        private byte[] HexToBytes(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                Convert.ToByte("", 16);
                result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
            }
            return result;
        }




        private string CRCForModbus(string data)
        {
                         // Handle digital conversion
            string sendBuf = data;
            string sendnoNull1 = sendBuf.Trim();// Remove spaces before and after the string
            string sendnoNull2 = sendnoNull1.Replace(" ", "");//Remove spaces in the middle of the string
            string sendNOComma = sendnoNull2.Replace(',', ' '); //Remove the English comma
            string sendNOComma1 = sendNOComma.Replace(',', ' '); //Remove Chinese comma
            string strSendNoComma2 = sendNOComma1.Replace("0x", ""); // Remove 0x
            string Data = strSendNoComma2.Replace("0X", ""); //Remove 0X

            byte[] crcbuf = strToToHexByte(data);


                         // Calculate and fill in the CRC check code
            Int32 crc = 0xffff;
            Int32 len = crcbuf.Length;
            for (Int32 n = 0; n < len; n++)
            {
                Byte i;
                crc = crc ^ crcbuf[n];
                for (i = 0; i < 8; i++)
                {
                    Int32 TT;
                    TT = crc & 1;
                    crc = crc >> 1;
                    crc = crc & 0x7fff;
                    if (TT == 1)
                    {
                        crc = crc ^ 0xa001;
                    }
                    crc = crc & 0xffff;
                }
            }
            crc = ((crc & 0xFF) << 8 | (crc >> 8));//High and low byte transposition
            String CRCString = crc.ToString("X2");

            return (data + CRCString);
        }

        /// <summary>
        /// hexadecimal string to byte array
        /// </summary>
        /// <param name="hexString">hex string</param>
        /// <returns> returns a byte array</returns>
        private Byte[] strToToHexByte(String hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = hexString.Insert(hexString.Length - 1, 0.ToString());
            Byte[] returnBytes = new Byte[hexString.Length / 2];
            for (Int32 i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        }
    }
