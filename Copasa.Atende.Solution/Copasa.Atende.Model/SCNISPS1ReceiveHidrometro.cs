using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCNISPS1ReceiveHidrometro - Lista hidrômetros de uma matrícula - Hidrômetro
    /// </summary>
    public class SCNISPS1ReceiveHidrometro : BaseModel
    {
        /// <summary>
        /// Tipo
        /// </summary>
        [XmlElement("_TIPO-PS")]
        public string tipo { get; set; }

        /// <summary>
        /// Número PS
        /// </summary>
        [XmlElement("_NUM-PS")]
        public string numeroPS { get; set; }

        /// <summary>
        /// Número medidor ABNT
        /// </summary>
        [XmlElement("_NUM-MEDIDOR-ABNT")]
        public string numeroMedidoABNT { get; set; }
    }
}
