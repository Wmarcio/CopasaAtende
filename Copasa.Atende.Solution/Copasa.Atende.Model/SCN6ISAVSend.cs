using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAVSend Altera vencimento fatura
    /// </summary>
    public class SCN6ISAVSend : BaseModelSend
    {
        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_RCV-IDENTIFICADOR")]
        public string identificador { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        [XmlElement("_RCV-NOME-SOLICITANTE")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// VencimentoAlternativo.
        /// </summary>
        [XmlElement("_RCV-DIA-VENCTO-ALTERNATIVO")]
        public string vencimentoAlternativo { get; set; }

        /// <summary>
        /// DocumentoSolicitante.
        /// </summary>
        [XmlElement("_RCV-NUM-DOCTO-SOLICITANTE")]
        public string documentoSolicitante { get; set; }

    }
}
