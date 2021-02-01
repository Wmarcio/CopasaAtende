using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAASend Lista agências atendimento
    /// </summary>
    public class SCN6ISAASend : BaseModelSend
    {
        /// <summary>
        /// Código da Localidade
        /// </summary>
        [XmlElement("_RCV-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// Código do Bairro
        /// </summary>
        [XmlElement("_RCV-COD-BAIRRO")]
        public string codigoBairro { get; set; }
    }
}
