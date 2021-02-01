using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISCP - Religação
    /// </summary>
    public class SCN4ISCPSend : BaseModelSend
    {
        /// <summary>
        /// Número da matrícula
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA")]
        public string numeroMatricula { get; set; }

        /// <summary>
        /// Identificador da unidade
        /// </summary>
        [XmlElement("_RCV-IDENT-UNID")]
        public string identificadorUnidade { get; set; }

        /// <summary>
        /// Servico SS
        /// </summary>
        [XmlElement("_RCV-SERVICO-SS")]
        public string servicoSS { get; set; }

        /// <summary>
        /// Valor da religação
        /// </summary>
        [XmlElement("_RCV-VALOR-RELIGACAO-N")]
        public string valorReligacao { get; set; }

        /// <summary>
        /// Valor de entrada do parcelamento
        /// </summary>
        [XmlElement("_RCV-VALOR-ENTRADA-PARCEL")]
        public string valorEntradaParcelamento { get; set; }
    }
}
