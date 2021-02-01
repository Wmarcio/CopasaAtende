using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFPSend Lista faturas pagas
    /// </summary>
    public class SCN6ISFPSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_NUM-IDENTIFICADOR")]
        public string identificador { get; set; }

        /// <summary>
        /// MesAnoReferencia.
        /// </summary>
        [XmlElement("_DT-REFERENCIA")]
        public string mesAnoReferencia { get; set; }
    }
}
