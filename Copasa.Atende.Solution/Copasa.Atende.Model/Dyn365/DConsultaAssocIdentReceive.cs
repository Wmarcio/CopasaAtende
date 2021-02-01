using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{

    /// <summary>
    /// Busca id de associação de identificador
    /// </summary>
    public class DConsultaAssocIdentReceive : BaseModelReceive
    {
        /// <summary>
        /// Id da associação cpf/cnpj e identificador
        /// </summary>
        [Dyn365Name("copasa_controledeidentificadorid")]
        public string ControleIdentificadorId { get; set; }       

    }
}
