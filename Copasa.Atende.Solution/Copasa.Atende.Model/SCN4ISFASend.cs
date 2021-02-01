using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISFASend Pesquisa interrupção de abastecimento
    /// </summary>
    public class SCN4ISFASend : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string empresa { get; set; }
    }
}
