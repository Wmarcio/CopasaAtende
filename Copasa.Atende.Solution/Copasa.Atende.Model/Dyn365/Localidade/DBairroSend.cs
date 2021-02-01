using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// busca de bairros de uma localidade
    /// </summary>
    public class DBairroSend : BaseModel
    {
        /// <summary>
        ///  Id da localidade
        /// </summary>
        public string IdLocalidade { get; set; }

    }
}
