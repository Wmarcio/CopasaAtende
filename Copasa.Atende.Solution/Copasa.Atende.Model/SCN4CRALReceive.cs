using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRALReceive - Gera eventos prioridade
    /// </summary>
    public class SCN4CRALReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-RETORNO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-MSG-RETORNO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// UnidadeResponsavel.
        /// </summary>
        [XmlElement("_SND-UNID-RESP")]
        public string unidadeResponsavel { get; set; }

        /// <summary>
        /// SuperintendenciaResponsavel.
        /// </summary>
        [XmlElement("_SND-UNID-SUP-RESP")]
        public string superintendenciaResponsavel { get; set; }

        /// <summary>
        /// DataGeracaoSS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-SS")]
        public string dataGeracaoSS { get; set; }

        /// <summary>
        /// HoraGeracaoSS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-SS")]
        public string horaGeracaoSS { get; set; }

        /// <summary>
        /// QuantidadeRT.
        /// </summary>
        [XmlElement("SND-QTDE-RT-SS")]
        public string quantidadeRT { get; set; }

        /// <summary>
        /// QuantidadeRE.
        /// </summary>
        [XmlElement("SND-QTDE-RE-SS")]
        public string quantidadeRE { get; set; }

        /// <summary>
        /// QuantidadePA.
        /// </summary>
        [XmlElement("_SND-QTDE-PA-SS")]
        public string quantidadePA { get; set; }

        /// <summary>
        /// QuantidadePR.
        /// </summary>
        [XmlElement("_SND-QTDE-PR-SS")]
        public string quantidadePR { get; set; }

        /// <summary>
        /// EmailUnidadeResponsavel.
        /// </summary>
        [XmlElement("_SND-EMAIL-UNID-RESP")]
        public string EmailUnidadeResponsavel { get; set; }

    }
}