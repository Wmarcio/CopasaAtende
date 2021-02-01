using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISMTSend - Busca matrícula pelo endereço
    /// </summary>
    public class SCN3ISMTSend : BaseModelSend
    {
        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_RCV-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_RCV-COD-BAIRRO")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_RCV-TIPO-LOGRADOURO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_RCV-COD-LOGRADOURO")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        [XmlElement("_RCV-NUM-IMOVEL")]
        [IS("N",5)]
        public string numeroImovel { get; set; }

        /// <summary>
        /// CodigoErro.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoErro { get; set; }
    }
}
