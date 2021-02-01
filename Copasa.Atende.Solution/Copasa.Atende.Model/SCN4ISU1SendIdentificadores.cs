using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4FTE1Send - Atualizar Email e telefone
    /// </summary>
    public class SCN4ISU1SendIdentificadores
    {
        /// <summary>
        /// Value
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
