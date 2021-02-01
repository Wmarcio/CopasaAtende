using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISTPReceive - Tarifa proporcional
    /// </summary>
    public class SCN6ISTPReceive : BaseModelReceive
    {
        /// <summary>
        /// DataLeituraAnterior.
        /// </summary>
        [XmlElement("_BK-DATA-LEITURA-ANT")]
        public string dataLeituraAnterior { get; set; }

        /// <summary>
        /// DataLeituraAtual.
        /// </summary>
        [XmlElement("_BK-DATA-LEITURA")]
        public string dataLeituraAtual { get; set; }

        /// <summary>
        /// PeriodoFatura.
        /// </summary>
        [XmlElement("_BK-PERIODO-FAT")]
        public string periodoFatura { get; set; }

        /// <summary>
        /// DataReajuste.
        /// </summary>
        [XmlElement("_BK-DATA-RJ")]
        public string dataReajuste { get; set; }

        /// <summary>
        /// VolumeFaturado.
        /// </summary>
        [XmlElement("_BK-FAT-VOL-FAT")]
        public string volumeFaturado { get; set; }

        /// <summary>
        /// PeriodoFaturadoSemReajuste.
        /// </summary>
        [XmlElement("_BK-FAT-PER-FAT-S-RJ-2")]
        public string periodoFaturadoSemReajuste { get; set; }

        /// <summary>
        /// PercentualFaturadoSemReajuste.
        /// </summary>
        [XmlElement("_BK-PERC-FAT-S-RJ")]
        public string percentualFaturadoSemReajuste { get; set; }

        /// <summary>
        /// ValorTarifaSemReajuste.
        /// </summary>
        [XmlElement("_BK-VLR-TAR-S-RJ")]
        public string valorTarifaSemReajuste { get; set; }

        /// <summary>
        /// PeriodoFaturadoComReajuste.
        /// </summary>
        [XmlElement("_BK-PER-FAT-C-RJ-2")]
        public string periodoFaturadoComReajuste { get; set; }

        /// <summary>
        /// PercentualFaturadoComReajuste.
        /// </summary>
        [XmlElement("_BK-PERC-FAT-C-RJ")]
        public string percentualFaturadoComReajuste { get; set; }

        /// <summary>
        /// ValorTarifaComReajuste.
        /// </summary>
        [XmlElement("_BK-VLR-TAR-COM-RJ")]
        public string valorTarifaComReajuste { get; set; }

        /// <summary>
        /// ValorTotalTarifa.
        /// </summary>
        [XmlElement("_BK-VLR-TOT-TARIFA")]
        public string valorTotalTarifa { get; set; }

        /// <summary>
        /// ValorTotalTarifa.
        /// </summary>
        [XmlElement("_BK-CABECALHO-CATEGORIA")]
        public string[] cabecalhoCategoria { get; set; }

        /// <summary>
        /// ValorTotalTarifa.
        /// </summary>
        [XmlElement("_TAB-DESC")]
        public string[] descricao { get; set; }
    }
}
