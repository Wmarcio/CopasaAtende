using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Mensagem de retorno de ws's
    /// </summary>
    public class DMensagemRetornoReceive : BaseModelReceive
    {
        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        public string Mensagem { get; set; }
    }
}
