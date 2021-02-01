using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CREXSend - Gera OS extra
    /// </summary>
    public class SCN4CREXSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-MATRICULA-CLIENTE")]
        public string matricula { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        [XmlElement("_RCV-NUM-PROTOCOLO")]
        public string numeroProtocolo { get; set; }

         /// <summary>
        /// CodigoUsuario.
        /// </summary>
        [XmlElement("_RCV-CD-USER")]
        public string codigoUsuario { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        [XmlElement("_RCV-NOME-USUARIO")]
        public string nomeUsuario { get; set; }

        /// <summary>
        /// UnidadeUsuario.
        /// </summary>
        [XmlElement("_RCV-UNIDADE-LOTACAO-USUARIO")]
        public string unidadeUsuario { get; set; }

    }
}
