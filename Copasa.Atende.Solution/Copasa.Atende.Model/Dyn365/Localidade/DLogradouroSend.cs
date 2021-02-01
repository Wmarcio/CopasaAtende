using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// busca de logradouros de um bairro
    /// </summary>
    public class DLogradouroSend : BaseModel
    {
        /// <summary>
        /// id do bairro
        /// </summary>
       public string BairroId { get; set; }
    }
}
