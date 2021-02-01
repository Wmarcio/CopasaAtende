using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCFSend Simula fatura
    /// </summary>
    public class SCN6ISCFSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-COD-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_RCV-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// SituacaoEsgoto.
        /// </summary>
        [XmlElement("_RCV-SITUACAO-ESGOTO")]
        public string situacaoEsgoto { get; set; }

        /// <summary>
        /// VolumeInformado.
        /// </summary>
        [XmlElement("_RCV-VOLUME-INFORMADO")]
        public string volumeInformado { get; set; }

        /// <summary>
        /// DataBaseTarifa.
        /// </summary>
        [XmlElement("_RCV-DT-BASE-TARIFA")]
        public string dataBaseTarifa { get; set; }

        /// <summary>
        /// QtdeEconomiasAguaResidencial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-RES-AGUA")]
        public string qtdeEconomiasAguaResidencial { get; set; }

        /// <summary>
        /// QtdeEconomiasAguaComercial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-COM-AGUA")]
        public string qtdeEconomiasAguaComercial { get; set; }

        /// <summary>
        /// QtdeEconomiasAguaIndustrial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-IND-AGUA")]
        public string qtdeEconomiasAguaIndustrial { get; set; }

        /// <summary>
        /// QtdeEconomiasAguaPublica.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-PUB-AGUA")]
        public string qtdeEconomiasAguaPublica { get; set; }

        /// <summary>
        /// QtdeEconomiasAguaSocial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-SOC-AGUA")]
        public string qtdeEconomiasAguaSocial { get; set; }

        /// <summary>
        /// QtdeEconomiasEsgotoResidencial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-RES-ESG")]
        public string qtdeEconomiasEsgotoResidencial { get; set; }

        /// <summary>
        /// QtdeEconomiasEsgotoComercial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-COM-ESG")]
        public string qtdeEconomiasEsgotoComercial { get; set; }

        /// <summary>
        /// QtdeEconomiasEsgotoIndustrial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-IND-ESG")]
        public string qtdeEconomiasEsgotoIndustrial { get; set; }

        /// <summary>
        /// QtdeEconomiasEsgotoPublica.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-PUB-ESG")]
        public string qtdeEconomiasEsgotoPublica { get; set; }

        /// <summary>
        /// QtdeEconomiasEsgotoSocial.
        /// </summary>
        [XmlElement("_RCV-QTDE-ECON-SOC-ESG")]
        public string qtdeEconomiasEsgotoSocial { get; set; }
    }
}
