using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISTPSend - Tarifa proporcional
    /// </summary>
    public class SCN6ISTPSend : BaseModelSend
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUMFATUR")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_REFERENCIA")]
        public string referencia { get; set; }

    }
}
