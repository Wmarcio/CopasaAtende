using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Dados de retorno da Associação de um Identificador no Microsoft Dynamics 365.
    /// </summary>
   public class DAssociaIdentificadorReceive : BaseModelReceive
    {
        /// <summary>
        /// Id da associação no Dyn365
        /// </summary>
        public string AssociacaoId { get; set; }
    }
}
