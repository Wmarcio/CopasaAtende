using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISQASend - Quitação anual de débito
    /// </summary>
    public class SCN6ISQASend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// AnoPesquisa.
        /// </summary>
        [XmlElement("_RCV-ANO-PESQUISA")]
        public string anoPesquisa { get; set; }
    }
}
