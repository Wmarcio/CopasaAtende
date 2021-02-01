using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Dyn365ValidaIdentificadorSend - Valida IdentificadoDyn365
    /// </summary>
    public class DConsultaIdentificadorSend : BaseModel
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        public string Identificador { get; set; }
    }
}
