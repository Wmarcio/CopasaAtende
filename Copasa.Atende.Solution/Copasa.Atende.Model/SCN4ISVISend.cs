using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISVISend Vazamento no imovel
    /// </summary>
    public class SCN4ISVISend : BaseModel
    {
        /// <summary>
        /// NumProtocolo.
        /// </summary>
        [XmlElement("_NUM-PROTOCOLO")]
        public string numProtocolo { get; set; }

        /// <summary>
        /// CodServico.
        /// </summary>
        [XmlElement("_COD-SERVICO")]
        public string codServico { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Solicitante.
        /// </summary>
        [XmlElement("_SOLICITANTE")]
        public string solicitante { get; set; }

        /// <summary>
        /// EntreRuas.
        /// </summary>
        [XmlElement("_ENTRE-RUAS")]
        public string entreRuas { get; set; }

        /// <summary>
        /// Telefone.
        /// </summary>
        [XmlElement("_TELEFONE")]
        public string telefone { get; set; }

        /// <summary>
        /// FlagOrigem.
        /// </summary>
        [XmlElement("_FLAG-ORIGEM")]
        public string flagOrigem { get; set; }

        /// <summary>
        /// FlagImagem.
        /// </summary>
        [XmlElement("_FLAG-IMAGEM")]
        public string flagImagem { get; set; }

    }
}
