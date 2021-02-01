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

        /// <summary>
        /// Retorna imagem de QRCode.
        /// </summary>
        public Stream retornaQRCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("teste",
            QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            MemoryStream memoryStream = new MemoryStream();
            qrCodeImage.Save(memoryStream, ImageFormat.Jpeg);
            long posicao = memoryStream.Position;
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
