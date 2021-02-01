using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISRE Tabela Parcelamento
    /// </summary>
    public class SCN4ISRESendParcelamento : BaseModel
    {
        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        [XmlElement("_RCV-NUM-PARCELAS")]
        public string numeroParcela { get; set; }

        /// <summary>
        /// Taxa de juros
        /// </summary>
        [XmlElement("_RCV-TX-JUROS")]
        public string taxaJuros { get; set; }

        /// <summary>
        /// Valor da Parcela
        /// </summary>
        [XmlElement("_RCV-VALOR-PARCELA")]
        public string valorParcela { get; set; }
    }
}
