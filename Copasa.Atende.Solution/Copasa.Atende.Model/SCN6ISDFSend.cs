using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDFSend Detalhe fatura
    /// </summary>
    public class SCN6ISDFSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_BRK-NUM-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// AnoMesReferencia.
        /// </summary>
        [XmlElement("_BRK-ANO-MES-REF")]
        public string anoMesReferencia { get; set; }
    }
}
