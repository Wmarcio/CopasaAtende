using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISVRSend Vazamento na rua
    /// </summary>
    public class SCN4ISVRReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_NUM-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// NumSS.
        /// </summary>
        [XmlElement("_NUM-SS-SEND")]
        public string numSS { get; set; }

        /// <summary>
        /// Total.
        /// </summary>
        [XmlElement("_TOTAL-SEND")]
        public string total { get; set; }

        /// <summary>
        /// DataLimite.
        /// </summary>
        [XmlElement("_DATA-LIMITE-SEND")]
        public string dataLimite { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_TIPO-LOGRADOURO-SEND")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// DescLogradouro.
        /// </summary>
        [XmlElement("_DESC-LOGRADOURO-SEND")]
        public string descLogradouro { get; set; }

        /// <summary>
        /// NumImovel.
        /// </summary>
        [XmlElement("_NUM-IMOVEL-SEND")]
        public string numImovel { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        [XmlElement("_TIPO-COMPLEMENTO-SEND")]
        public string tipoComplemento { get; set; }

        /// <summary>
        /// Complemento.
        /// </summary>
        [XmlElement("_COMPLEMENTO-SEND")]
        public string complemento { get; set; }

        
        /// <summary>
        /// DescBairro.
        /// </summary>
        [XmlElement("_DESC-BAIRRO-SEND")]
        public string descBairro { get; set; }
        
        /// <summary>
        /// DescLocalidade.
        /// </summary>
        [XmlElement("_DESC-LOCALIDADE-SEND")]
        public string descLocalidade { get; set; }
        
        /// <summary>
        /// NumOS.
        /// </summary>
        [XmlElement("_NUM-OS-SEND")]
        public string numOS { get; set; }
        
        /// <summary>
        /// Distrito.
        /// </summary>
        [XmlElement("_DISTRITO-SEND")]
        public string distrito { get; set; }
        
        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        [XmlElement("_DATA-GERACAO-OS-SEND")]
        public string dataGeracaoOS { get; set; }
        
        /// <summary>
        /// DataExecOS.
        /// </summary>
        [XmlElement("_DATA-EXEC-OS-SEND")]
        public string dataExecOS { get; set; }
        
        /// <summary>        
        /// CodServico.
        /// </summary>
        [XmlElement("_COD-SERVICO-SEND")]
        public string codServico { get; set; }
        
        /// <summary>
        /// SituacaoOS.
        /// </summary>
        [XmlElement("_SITUACAO-OS-SEND")]
        public string situacaoOS { get; set; }
    }
}
