using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS02Send Informar leitura - Envia leitura
    /// </summary>
    public class SCN5IS02Send : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// NumProtocolo.
        /// </summary>
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        [XmlElement("_DATA-LEITURA")]
        public string dataLeitura { get; set; }

        /// <summary>
        /// DataRef.
        /// </summary>
        [XmlElement("_DATAREF")]
        public string dataReferencia { get; set; }

        /// <summary>
        /// NomeInformante.
        /// </summary>
        [XmlElement("_NOME-INFORMANTE")]
        public string solicitante { get; set; }

        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// Medidores
        /// </summary>
        [XmlElement("_TABELAS")]
        public SCN5IS02SendTabelas[] medidores { get; set; }
    }
}
