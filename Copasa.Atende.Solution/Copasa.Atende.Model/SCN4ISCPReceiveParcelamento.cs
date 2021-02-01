using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISCP Receive - tabela Parcelamento
    /// </summary>
    public class SCN4ISCPReceiveParcelamento : BaseModel
    {
        /// <summary>
        /// Número de Parcelas
        /// </summary>
        [XmlElement("_SND-NUM-PARCELAS")]
        public string numeroParcelas { get; set; }

        /// <summary>
        /// Taxa de Juros
        /// </summary>
        [XmlElement("_SND-TX-JUROS")]
        public string taxaJuros { get; set; }

        /// <summary>
        /// Valor da Parcela
        /// </summary>
        [XmlElement("_SND-VALOR-PARCELA")]
        public string valorParcela { get; set; }
    }
}
