using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISCSReceive - busca informações de matrícula
    /// </summary>
    public class SCN4ISCSReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroProtocolo
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-CRM")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// NumeroSS
        /// </summary>
        [XmlElement("_SND-NRO-SOLIC-SERV")]
        public string numeroSS { get; set; }

        /// <summary>
        /// CodigoServicoInsumo
        /// </summary>
        [XmlElement("_SND-COD-SERV-INSUMO")]
        public string codigoServicoInsumo { get; set; }

        /// <summary>
        /// ClasseServico
        /// </summary>
        [XmlElement("_SND-CLASSE-SERVICO")]
        public string classeServico { get; set; }

        /// <summary>
        /// EnviaCRM
        /// </summary>
        [XmlElement("_SND-FLAG-ENVIA-CRM")]
        public string enviaCRM { get; set; }

        /// <summary>
        /// DataPrevisao
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-SS")]
        public string dataPrevisao { get; set; }

        /// <summary>
        /// HoraPrevisao
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-SS")]
        public string horaPrevisao { get; set; }

        /// <summary>
        /// NumeroOSAtual
        /// </summary>
        [XmlElement("_SND-NRO-OS-ATUAL")]
        public string numeroOSAtual { get; set; }

        /// <summary>
        /// QtdeEconomiaResisdAgua
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-RES-AGUA")]
        public string qtdeEconomiaResisdAgua { get; set; }

        /// <summary>
        /// QtdeEconomiaSocialAgua
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-SOC-AGUA")]
        public string qtdeEconomiaSocialAgua { get; set; }

        /// <summary>
        /// QtdeEconomiaComercialAgua
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-COM-AGUA")]
        public string qtdeEconomiaComercialAgua { get; set; }

        /// <summary>
        /// QtdeEconomiaIndustrialAgua
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-IND-AGUA")]
        public string qtdeEconomiaIndustrialAgua { get; set; }

        /// <summary>
        /// QtdeEconomiaPublicaAgua
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-PUB-AGUA")]
        public string qtdeEconomiaPublicaAgua { get; set; }

        /// <summary>
        /// QtdeEconomiaResidEsgoto
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-RES-ESG")]
        public string qtdeEconomiaResidEsgoto { get; set; }

        /// <summary>
        /// QtdeEconomiaSocialEsgoto
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-SOC-ESG")]
        public string qtdeEconomiaSocialEsgoto { get; set; }

        /// <summary>
        /// QtdeEconomiaComercialEsgoto
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-COM-ESG")]
        public string qtdeEconomiaComercialEsgoto { get; set; }

        /// <summary>
        /// QtdeEconomiaIndustrialEsgoto
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-IND-ESG")]
        public string qtdeEconomiaIndustrialEsgoto { get; set; }

        /// <summary>
        /// QtdeEconomiaPublicoEsgoto
        /// </summary>
        [XmlElement("_SND-QTDE-ECON-PUB-ESG")]
        public string qtdeEconomiaPublicoEsgoto { get; set; }

        /// <summary>
        /// ValorServico
        /// </summary>
        [XmlElement("_SND-VALOR-SERVICO-INFO")]
        public string valorServico { get; set; }
    }
}
