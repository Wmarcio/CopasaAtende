using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISPDReceive - Consulta parcelamento de débito Fatura
    /// </summary>
    public class SCN6ISPDReceiveFatura : BaseModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_SND-NUM-FATURA")]
        public string numero { get; set; }
        /// <summary>
        /// AnoMesReferencia.
        /// </summary>
        [XmlElement("_SND-DATA-REFERENCIA")]
        public string anoMesReferencia { get; set; }
        /// <summary>
        /// Provisao.
        /// </summary>
        [XmlElement("_SND-FLAG-PROVISAO")]
        public string provisao { get; set; }
        /// <summary>
        /// ValorTotal.
        /// </summary>
        [XmlElement("_SND-VALOR-FAT-TOTAL")]
        public string valorTotal { get; set; }
        /// <summary>
        /// ValorJuros.
        /// </summary>
        [XmlElement("_SND-VALOR-FAT-JUROS")]
        public string valorJuros { get; set; }
        /// <summary>
        /// ValorAtual.
        /// </summary>
        [XmlElement("_SND-VALOR-FAT-ATUAL")]
        public string valorAtual { get; set; }
        /// <summary>
        /// ValorParcela.
        /// </summary>
        [XmlElement("_SND-VALOR-FAT-PARCE")]
        public string valorParcela { get; set; }
        /// <summary>
        /// ValorMulta.
        /// </summary>
        [XmlElement("_SND-VALOR-FAT-MULTA")]
        public string valorMulta { get; set; }
    }
}
