using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Lista de Financiamentos - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNReceiveFinanciamento : BaseModelReceive
    {
        /// <summary>
        /// Quantidade de parcelas de financiamento
        /// </summary>
        [XmlElement("_PARCELAS-FINANC")]
        public string parcelasFinanciamento { get; set; }

        /// <summary>
        /// Valor restante do financiamento
        /// </summary>
        [XmlElement("_VLR-RESTANTE-FINANC")]
        public string valorRestanteFinanciamento { get; set; }

        /// <summary>
        /// Valor de parcelas do financiamento
        /// </summary>
        [XmlElement("_VALOR-PARCELA-FINANC")]
        public string valorParcelaFinanciamento { get; set; }
    }
}
