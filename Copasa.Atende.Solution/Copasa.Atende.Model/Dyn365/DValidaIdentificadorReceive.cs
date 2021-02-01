using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Dyn365ValidaCpfCnpjReceive - Recupera IdentificadorDyn365
    /// </summary>
    public class DValidaIdentificadorReceive : BaseModelReceive
    {
        /// <summary>
        /// Id do Identificador.
        /// </summary>
        public List<DIdentificador> IdentificadorId { get; set; }
    }
}
