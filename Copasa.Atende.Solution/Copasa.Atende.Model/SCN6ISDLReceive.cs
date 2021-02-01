using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDLReceive - Calendário faturamento
    /// </summary>
    public class SCN6ISDLReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-MENSAGEM")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_MENSAGEM")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA-ENV")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_NUM-IDENTIFICADOR-ENV")]
        public string identificador { get; set; }

        /// <summary>
        /// Cliente.
        /// </summary>
        [XmlElement("_NOMECLIENTE-ENV")]
        public string nome { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_TIPOLOGRADOURO-ENV")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// Logradouro.
        /// </summary>
        [XmlElement("_NOMELOGRADOURO-ENV")]
        public string logradouro { get; set; }

        /// <summary>
        /// numero.
        /// </summary>
        [XmlElement("_NUMIMOVEL-ENV")]
        public string numero { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        [XmlElement("_TIPOCOMPLEMENTO-ENV")]
        public string tipoComplemento { get; set; }

        /// <summary>
        /// Complemento.
        /// </summary>
        [XmlElement("_INFCOMPLEMENTO-ENV")]
        public string complemento { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_NOMEBAIRRO-ENV")]
        public string bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [XmlElement("_NOMELOCALIDADE-ENV")]
        public string localidade { get; set; }

        /// <summary>
        /// AnoReferencia.
        /// </summary>
        [XmlElement("_ANO-REFERENCIA-ENV")]
        public string anoReferencia { get; set; }

        /// <summary>
        /// DadosSicom.
        /// </summary>
        [XmlElement("_DADOS")]
        [JsonIgnore]
        public SCN6ISDLReceiveDado[] dadosSicom { get; set; }

        /// <summary>
        /// dados.
        /// </summary>
        public List<SCN6ISDLReceiveDado> dados { get; set; }
    }
}
