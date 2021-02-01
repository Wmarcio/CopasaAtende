using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{

    /// <summary>
    /// retorno da criação de um protocolo
    /// </summary>
    public class DCriaProtocoloReceive : BaseModelReceive
    {
        /// <summary>
        /// Id do protocolo
        /// </summary>
        [Dyn365Name("incidentid")]
        public string ProtocoloId { get; set; }

        /// <summary>
        /// Id do protocolo
        /// </summary>
        [Dyn365Name("copasa_protocolo")]
        public string Protocolo { get; set; }
    }
}
