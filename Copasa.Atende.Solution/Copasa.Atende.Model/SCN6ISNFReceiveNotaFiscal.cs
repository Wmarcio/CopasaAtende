using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISNFReceiveNotaFiscal - Nota fiscal fatura
    /// </summary>
    public class SCN6ISNFReceiveNotaFiscal : BaseModel
    {
        /// <summary>
        /// CodigoVerificacao.
        /// </summary>
        [XmlElement("_COD-VERIFICACAO")]
        public string codigoVerificacao { get; set; }

        /// <summary>
        /// NotaFiscalEletronica.
        /// </summary>
        [XmlElement("_NOTA-FISCAL-ELETRONICA")]
        public string notaFiscalEletronica { get; set; }
    }
}
