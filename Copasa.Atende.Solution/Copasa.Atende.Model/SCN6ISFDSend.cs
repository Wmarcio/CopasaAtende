using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFDSend Lista faturas em débito
    /// </summary>
    public class SCN6ISFDSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_IDENTIFICADOR")]
        public string identificador { get; set; }

        /// <summary>
        /// MesAnoPeriodo.
        /// </summary>
        [XmlElement("_PERIODO-PARM-A")]
        public string mesAnoPeriodo { get; set; }
    }
}
