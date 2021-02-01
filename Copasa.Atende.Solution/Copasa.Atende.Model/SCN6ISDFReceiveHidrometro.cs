using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDFReceiveHidrometro Detalhe fatura
    /// </summary>
    public class SCN6ISDFReceiveHidrometro : BaseModel
    {
        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_BRK-NUM-HIDROMETRO")]
        public string numero { get; set; }

        /// <summary>
        /// DataInstalacao.
        /// </summary>
        [XmlElement("_BRK-DT-INST-HIDROMETRO")]
        public string dataInstalacao { get; set; }

        /// <summary>
        /// LeituraAnterior.
        /// </summary>
        [XmlElement("_BRK-LEITURA-ANTERIOR")]
        public string leituraAnterior { get; set; }

        /// <summary>
        /// DataLeituraAnterior.
        /// </summary>
        [XmlElement("_BRK-DT-LEITURA-ANT")]
        public string dataLeituraAnterior { get; set; }

        /// <summary>
        /// LeituraAtual.
        /// </summary>
        [XmlElement("_BRK-LEITURA-ATUAL")]
        public string leituraAtual { get; set; }

        /// <summary>
        /// DataLeituraAtual.
        /// </summary>
        [XmlElement("_BRK-DT-LEITURA-ATUAL")]
        public string dataLeituraAtual { get; set; }

        /// <summary>
        /// DataProximaLeitura.
        /// </summary>
        [XmlElement("_BRK-DT-PROXIMA-LEITURA")]
        public string dataProximaLeitura { get; set; }
    }
}
