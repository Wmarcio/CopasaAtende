using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Lista de Parcelamento - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNReceiveParcelamento : BaseModel
    {
        /// <summary>
        /// Matricula
        /// </summary>
        [XmlElement("_MATRICULA-PARC")]
        public string matricula { get; set; }

        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        [XmlElement("_PARCELAS-PARC")]
        public string parcelasParcial { get; set; }

        /// <summary>
        /// Valor restante do parcelamento
        /// </summary>
        [XmlElement("_VLR-RESTANTE-PARC")]
        public string valorRestanteParcial { get; set; }

        /// <summary>
        /// Valor total do parcelamento
        /// </summary>
        [XmlElement("_VALOR-PARCELA-PARC")]
        public string valorParcelaParcial { get; set; }
    }
}
