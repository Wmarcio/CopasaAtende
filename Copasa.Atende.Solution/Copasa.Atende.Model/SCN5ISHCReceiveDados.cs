using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5ISHCReceiveDados Histórico de consumo
    /// </summary>
    public class SCN5ISHCReceiveDados : BaseModel
    {
        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_REFERENCIA")]
        public string referencia { get; set; }

        /// <summary>
        /// leitura do hidrometro
        /// </summary>
        [XmlElement("_LEITURA-REAL-A")]
        public string leitura { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        [XmlElement("_DATA-LEITURA")]
        public string dataLeitura { get; set; }

        /// <summary>
        /// Volume.
        /// </summary>
        [XmlElement("_VOLUME-FATURADO-A")]
        public string volume { get; set; }

        /// <summary>
        /// MediaConsumo.
        /// </summary>
        [XmlElement("_MEDIA-CONSUMO-A")]
        public string mediaConsumo { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        [XmlElement("_VALOR-FATURADO-A")]
        public string valor { get; set; }

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
    }
}
