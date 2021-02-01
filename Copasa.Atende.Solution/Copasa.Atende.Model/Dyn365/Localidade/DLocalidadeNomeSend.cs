using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Paramentros de entrada da busca de localidades
    /// </summary>
   public class DLocalidadeNomeSend : BaseModel
    {
        /// <summary>
        ///  COPASA/COPANOR
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        ///  COPASA/COPANOR
        /// </summary>
        public string NomeLocalidade { get; set; }

    }
}
