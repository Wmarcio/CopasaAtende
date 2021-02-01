using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using System.IO;
using System.Net;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - Fatura.
    /// </summary>
    public interface IFaturaRule
    {
        /// <summary>
        /// Lista faturas em débito.
        /// </summary>
        BaseResponse SCN6ISFD(SCN6ISFDSend sCN6ISFDSend,bool pesquisaTodosDados);

        /// <summary>
        /// Lista faturas pagas.
        /// </summary>
        BaseResponse SCN6ISFP(SCN6ISFPSend sCN6ISFPSend);

        /// <summary>
        /// Detalhe fatura.
        /// </summary>
        BaseResponse SCN6ISDF(SCN6ISDFSend sCN6ISDFSend);

        /// <summary>
        /// Gera parcelamento de débito.
        /// </summary>
        BaseResponse SCN6ISGP(SCN6ISGPSend sCN6ISGPSend);

        /// <summary>
        /// Consulta parcelamento de débito.
        /// </summary>
        BaseResponse SCN6ISPD(SCN6ISPDSend sCN6ISPDSend);

        /// <summary>
        /// Nota fiscal fatura.
        /// </summary>
        BaseResponse SCN6ISNF(SCN6ISNFSend sCN6ISNFSend);

        /// <summary>
        /// Simula fatura.
        /// </summary>
        BaseResponse SCN6ISCF(SCN6ISCFSend sCN6ISCFSend);

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        WebResponse retornaFaturaPDF(string numFatura);

        /// <summary>
        /// Retorna fatura em formato PDF.
        /// </summary>
        BaseResponse retornaFaturaPDF(SCN6EFEMSend sCN6EFEMSend);

        /// <summary>
        /// Retorna imagem do código de barras.
        /// </summary>
        Stream retornaCodigoBarras(string codigo);

        /// <summary>
        /// Retorna imagem de QRCode.
        /// </summary>
        Stream retornaQRCode();

        /// <summary>
        /// Tarifa proporcional.
        /// </summary>
        BaseResponse SCN6ISTP(SCN6ISTPSend sCN6ISTPSend);
    }
}
