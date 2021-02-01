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
    /// SCN5IS02Send Informar leitura - Envia leitura / tabela de leituras
    /// </summary>
    public class SCN5IS02SendTabelas : BaseModel
    {
        /// <summary>
        /// NumMedidorABNT.
        /// </summary>
        [XmlElement("_NUM-MEDIDOR-ABNT")]
        public string medidor { get; set; }

        /// <summary>
        /// Leitura.
        /// </summary>
        [XmlElement("_LEITURA")]
        public string leitura { get; set; }

    }
}
