using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using System.IO;
using System.Net;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Fatura.
    /// </summary>
    public class FaturaFacade : IFaturaFacade
    {
        private IFaturaRule _faturaRule;

        /// <summary>
        /// Construtor FaturaFacade.
        /// </summary>
        /// <param name="faturaRule">FaturaRule.</param>
        public FaturaFacade(IFaturaRule faturaRule)
        {
            _faturaRule = faturaRule;
        }

        /// <summary>
        /// Lista faturas em débito.
        /// </summary>
        public BaseResponse SCN6ISFD(SCN6ISFDSend sCN6ISFDSend)
        {
            return _faturaRule.SCN6ISFD(sCN6ISFDSend, true);
        }

        /// <summary>
        /// Lista faturas pagas.
        /// </summary>
        public BaseResponse SCN6ISFP(SCN6ISFPSend sCN6ISFPSend)
        {
            return _faturaRule.SCN6ISFP(sCN6ISFPSend);
        }

        /// <summary>
        /// Detalhe fatura.
        /// </summary>
        public BaseResponse SCN6ISDF(SCN6ISDFSend sCN6ISDFSend)
        {
            return _faturaRule.SCN6ISDF(sCN6ISDFSend);
        }

        /// <summary>
        /// Gera parcelamento de débito.
        /// </summary>
        public BaseResponse SCN6ISGP(SCN6ISGPSend sCN6ISGPSend)
        {
            return _faturaRule.SCN6ISGP(sCN6ISGPSend);
        }

        /// <summary>
        /// Consulta parcelamento de débito.
        /// </summary>
        public BaseResponse SCN6ISPD(SCN6ISPDSend sCN6ISPDSend)
        {
            return _faturaRule.SCN6ISPD(sCN6ISPDSend);
        }

        /// <summary>
        /// Nota fiscal fatura.
        /// </summary>
        public BaseResponse SCN6ISNF(SCN6ISNFSend sCN6ISNFSend)
        {
            return _faturaRule.SCN6ISNF(sCN6ISNFSend);
        }

        /// <summary>
        /// Simula fatura.
        /// </summary>
        public BaseResponse SCN6ISCF(SCN6ISCFSend sCN6ISCFSend)
        {
            return _faturaRule.SCN6ISCF(sCN6ISCFSend);
        }

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        public WebResponse retornaFaturaPDF(string numeroFatura)
        {
            return _faturaRule.retornaFaturaPDF(numeroFatura);
        }

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        public BaseResponse retornaFaturaPDF(SCN6EFEMSend sCN6EFEMSend)
        {
            return _faturaRule.retornaFaturaPDF(sCN6EFEMSend);
        }

        /// <summary>
        /// Retorna imagem do código de barras.
        /// </summary>
        public Stream retornaCodigoBarras(string codigo)
        {
            return _faturaRule.retornaCodigoBarras(codigo);
        }

        /// <summary>
        /// Retorna imagem de QRCode.
        /// </summary>
        public Stream retornaQRCode()
        {
            return _faturaRule.retornaQRCode();
        }

        /// <summary>
        /// Tarifa proporcional.
        /// </summary>
        public BaseResponse SCN6ISTP(SCN6ISTPSend sCN6ISTPSend)
        {
            return _faturaRule.SCN6ISTP(sCN6ISTPSend);
        }
    }
}
