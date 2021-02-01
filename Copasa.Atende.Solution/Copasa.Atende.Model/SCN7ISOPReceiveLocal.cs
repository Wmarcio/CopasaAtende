using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN7ISOPReceiveLocal - Onde pagar a conta
    /// </summary>
    public class SCN7ISOPReceiveLocal : BaseModel
    {
        /// <summary>
        /// Estabelecimento.
        /// </summary>
        [XmlElement("_ESTABELECIMENTO")]
        public string estabelecimento { get; set; }

        /// <summary>
        /// Endereco.
        /// </summary>
        [XmlElement("_ENDERECO")]
        public string endereco { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_BAIRRO")]
        public string bairro { get; set; }

    }
}
