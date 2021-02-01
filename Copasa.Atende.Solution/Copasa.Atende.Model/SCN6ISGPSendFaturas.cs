using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISGPSend - Gera parcelamento de débito
    /// </summary>
    public class SCN6ISGPSendFaturas : BaseModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_RCV-NUM-FATURA")]
        public string numero { get; set; }
        /// <summary>
        /// AnoMesReferencia.
        /// </summary>
        [XmlElement("_RCV-DATA-REFERENCIA")]
        public string anoMesReferencia { get; set; }
        /// <summary>
        /// Provisao.
        /// </summary>
        [XmlElement("_RCV-FLAG-PROVISAO")]
        public string provisao { get; set; }
        /// <summary>
        /// ValorTotal.
        /// </summary>
        [XmlElement("_RCV-VALOR-FAT-TOTAL")]
        public string valorTotal { get; set; }
        /// <summary>
        /// ValorJuros.
        /// </summary>
        [XmlElement("_RCV-VALOR-FAT-JUROS")]
        public string valorJuros { get; set; }
        /// <summary>
        /// ValorAtual.
        /// </summary>
        [XmlElement("_RCV-VALOR-FAT-ATUAL")]
        public string valorAtual { get; set; }
        /// <summary>
        /// ValorParcela.
        /// </summary>
        [XmlElement("_RCV-VALOR-FAT-PARCE")]
        public string valorParcela { get; set; }
        /// <summary>
        /// ValorMulta.
        /// </summary>
        [XmlElement("_RCV-VALOR-FAT-MULTA")]
        public string valorMulta { get; set; }
    }
}
