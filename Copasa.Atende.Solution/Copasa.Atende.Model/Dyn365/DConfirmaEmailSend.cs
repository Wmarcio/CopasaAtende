using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Parametros de entrada da confirmação
    /// </summary>
    public class DConfirmaEmailSend : BaseModel
    {
        /// <summary>
        /// id do protocolo
        /// </summary>
        public string ProtocoloId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
