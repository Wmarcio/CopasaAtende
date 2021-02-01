using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISQAReceiveFaturaEmDebito - Quitação anual de débito - Faturas em débito
    /// </summary>
    public class SCN6ISQAReceiveFaturaEmDebito : BaseModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_SND-NRO-FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_SND-DATA-REF-FATURA")]
        public string referencia { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        [XmlElement("_SND-VALOR-FATURA")]
        public string valor { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        [XmlElement("_SDN-DATA-VENCTO-FATURA")]
        public string dataVencimento { get; set; }

        /// <summary>
        /// Endereco.
        /// </summary>
        public string endereco { get; set; }
    }
}
