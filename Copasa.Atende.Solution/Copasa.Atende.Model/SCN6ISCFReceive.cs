using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCFReceive Simula fatura
    /// </summary>
    public class SCN6ISCFReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// GrupoTarifario
        /// </summary>
        [XmlElement("_SND-GRUPO-TARIF")]
        public string grupoTarifario { get; set; }

        /// <summary>
        /// TipoTarifa.
        /// </summary>
        [XmlElement("_SND-TIPO-TARIFA")]
        public string tipoTarifa { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaResidencial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-RES-AGUA")]
        public string valorFaturamentoAguaResidencial { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaComercial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-COM-AGUA")]
        public string valorFaturamentoAguaComercial { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaIndustrial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-IND-AGUA")]
        public string valorFaturamentoAguaIndustrial { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaPublica.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-PUB-AGUA")]
        public string valorFaturamentoAguaPublica { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaSocial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-SOC-AGUA")]
        public string valorFaturamentoAguaSocial { get; set; }

        /// <summary>
        /// ValorFaturamentoAguaTotal.
        /// </summary>
        [XmlElement("_SND-TOTAL-FAT-AGUA")]
        public string valorFaturamentoAguaTotal { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoResidencial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-RES-ESG")]
        public string valorFaturamentoEsgotoResidencial { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoComercial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-COM-ESG")]
        public string valorFaturamentoEsgotoComercial { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoIndustrial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-IND-ESG")]
        public string valorFaturamentoEsgotoIndustrial { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoPublica.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-PUB-ESG")]
        public string valorFaturamentoEsgotoPublica { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoSocial.
        /// </summary>
        [XmlElement("_SND-SOMA-FAT-SOC-ESG")]
        public string valorFaturamentoEsgotoSocial { get; set; }

        /// <summary>
        /// ValorFaturamentoEsgotoTotal.
        /// </summary>
        [XmlElement("_SND-TOTAL-FAT-ESGOTO")]
        public string valorFaturamentoEsgotoTotal { get; set; }

        /// <summary>
        /// ValorTotalFatura.
        /// </summary>
        [XmlElement("_SND-TOTAL-FATURA")]
        public string valorTotalFatura { get; set; }
    }
}
