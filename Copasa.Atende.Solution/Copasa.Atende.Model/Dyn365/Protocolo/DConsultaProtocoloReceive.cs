using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// 
    /// </summary>
    public class DConsultaProtocoloReceive : BaseModelReceive
    {
        /// <summary>
        /// Descrição
        /// </summary>
        [Dyn365Name("description")]
        public string Descricao { get; set; }
    }
}
