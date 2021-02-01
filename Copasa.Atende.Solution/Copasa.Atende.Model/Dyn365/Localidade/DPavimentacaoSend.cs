using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{

    /// <summary>
    /// Consulta lista de pavimentação
    /// </summary>
    public class DPavimentacaoSend : BaseModel
    {
        /// <summary>
        /// codigo do subtipo
        /// </summary>
        public string Subtipo { get; set; }

        /// <summary>
        /// COPASA/COPANOR
        /// </summary>
        public string Empresa { get; set; }
    }
}
