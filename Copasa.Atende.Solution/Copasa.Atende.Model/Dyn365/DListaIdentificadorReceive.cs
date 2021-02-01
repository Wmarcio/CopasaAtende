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
    public class DListaIdentificadorReceive : BaseModelReceive
    {
        /// <summary>
        /// Id do Identificador.
        /// </summary>
        public List<DIdentificador> Identificador { get; set; }
    }

    /// <summary>
    /// Dyn365User - Usuario Dynamics
    /// </summary>
    public class DIdentificador : BaseModel
    {
        /// <summary>
        /// Nome/codigo do identificador do D365
        /// </summary>
        [Dyn365Name("copasa_name")]
        public string Identificador { get; set; }

    }
}
