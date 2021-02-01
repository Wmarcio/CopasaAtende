using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFPReceiveContas Lista faturas pagas
    /// </summary>
    public class SCN6ISFPReceiveContas : BaseModel
    {
        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_REFERENCIA")]
        public string referencia { get; set; }

        /// <summary>
        /// NumFatura.
        /// </summary>
        [XmlElement("_NUM-FATURA")]
        public string numFatura { get; set; }

        /// <summary>
        /// ValorTotalFatura.
        /// </summary>
        [XmlElement("_VALOR-TOTAL-FATURA")]
        public string valorTotalFatura { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        [XmlElement("_DATA-VENCIMENTO")]
        public string dataVencimento { get; set; }

        /// <summary>
        /// DataPagamento.
        /// </summary>
        [XmlElement("_DATA-PAGAMENTO")]
        public string dataPagamento { get; set; }

        /// <summary>
        /// CodigoBanco.
        /// </summary>
        [XmlElement("_COD-BANCO")]
        public string codigoBanco { get; set; }

        /// <summary>
        /// Retificada.
        /// </summary>
        [XmlElement("_RETIFICA")]
        public string retificada { get; set; }

    }
}
