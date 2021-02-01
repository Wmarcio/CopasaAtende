using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCESend Atualiza status para envio conta por email
    /// </summary>
    public class SCN6ISCESend : BaseModelSend
    {
        /// <summary>
        /// Matriculas.
        /// </summary>
        [XmlArray("Array")]
        [XmlArrayItem("_TABELA-MAT")]
        public SCN6ISCESendMatricula[] matriculas { get; set; }
    }
}
