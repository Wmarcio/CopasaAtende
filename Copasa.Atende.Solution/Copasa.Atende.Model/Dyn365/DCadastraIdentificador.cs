using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Retorno do cadastro de Identificador do dynamics 365
    /// </summary>
    public class DCadastraIdentificador : BaseModel
    {
        /// <summary>
        /// Id do identificador do Dyn365
        /// </summary>
        [Dyn365Name("id")]
        public string IdentificadorId { get; set; }

        /// <summary>
        /// Memsagem de retorno.
        /// </summary>
        [Dyn365Name("message")]
        public string Mensagem { get; set; }

    }
}
