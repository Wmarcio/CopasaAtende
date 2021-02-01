using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISMTReceive - Busca matrícula pelo endereço
    /// </summary>
    public class SCN3ISMTReceiveMatricula : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_SND-NUM-MATRICULA-ATUAL")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador do usuario
        /// </summary>
        [XmlElement("_SND-IDENTIFICADOR-USUARIO")]
        public string identificador { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_SND-COD-LOGR-MAT-ATUAL")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_SND-TIPO-LOGR-MAT-ATUAL")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [XmlElement("_SND-NOME-LOGR-MAT-ATUAL")]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        [XmlElement("_SND-NUM-IMOVEL-MAT-ATUAL")]
        public string numeroImovel { get; set; }

        /// <summary>
        /// TipoComplementoImovel.
        /// </summary>
        [XmlElement("_SND-COMPL-TIPO-MAT-ATUAL")]
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        [XmlElement("_SND-COMPL-INF-MAT-ATUAL")]
        public string complementoImovel { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_SND-BAIRRO-MAT-ATUAL")]
        public string bairro { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_SND-COD-BAIRRO-MAT-ATUAL")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        [XmlElement("_SND-NOME-LOCAL-MAT-ATUAL")]
        public string nomeLocalidade { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_SND-COD-LOCALIDADE-MAT-ATUAL")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>
        [XmlElement("_SND-CEP-MAT-ATUAL")]
        public string CEP { get; set; }

    }
}
