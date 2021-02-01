using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISRE Tabela de contas
    /// </summary>
    public class SCN4ISRESendContas : BaseModel
    {
        /// <summary>
        /// Mês referência
        /// </summary>
        [XmlElement("_RCV-REFERENCIA")]
        public string referencia { get; set; }

        /// <summary>
        /// Número da fatura
        /// </summary>
        [XmlElement("_RCV-NUM-FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// Valor total da fatura
        /// </summary>
        [XmlElement("_RCV-VLR-TOTAL-FATURA")]
        public string valorTotalFatura { get; set; }

        /// <summary>
        /// Data de vencimento
        /// </summary>
        [XmlElement("_RCV-VENCIMENTO")]
        public string vencimento { get; set; }
    }
}
