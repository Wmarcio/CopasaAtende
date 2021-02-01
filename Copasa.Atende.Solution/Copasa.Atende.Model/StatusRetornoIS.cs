using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// IS Receive.
    /// </summary>
    public class StatusRetornoIS : BaseModel
    {
        /// <summary>
        /// Codigo.
        /// </summary>
        [XmlElement("_SND-COD-RETORNO")]
        public int Codigo { get; set; }

        /// <summary>
        /// Descricao.
        /// </summary>
        [XmlElement("_SND-MSG-RETORNO")]
        public string Descricao { get; set; }

    }
}
