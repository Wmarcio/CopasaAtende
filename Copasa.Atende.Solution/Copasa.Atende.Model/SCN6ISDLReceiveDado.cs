using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDLReceiveDado - Enviar serviço
    /// </summary>
    public class SCN6ISDLReceiveDado : BaseModel
    {
        /// <summary>
        /// MesReferencia.
        /// </summary>
        [XmlElement("_MES-REFERENCIA-ENV")]
        public string mesReferencia { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        [XmlElement("_DATA-LEITURA-ENV")]
        public string dataLeitura { get; set; }

        /// <summary>
        /// DataEnvio.
        /// </summary>
        [XmlElement("_DATA-ENVIO-ENV")]
        public string dataEnvio { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        [XmlElement("_DATA-VENC-ENV")]
        public string dataVencimento { get; set; }

    }
}
