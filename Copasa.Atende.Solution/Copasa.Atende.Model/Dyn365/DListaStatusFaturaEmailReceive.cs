using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// 
    /// </summary>
   public class DListaStatusFaturaEmailReceive : BaseModelReceive
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DStatusFaturaEmail> StatusFaturasEmail { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DStatusFaturaEmail : BaseModel
    {
        /// <summary>
        /// identificador
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Matricula
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// vazio/S/N
        /// </summary>
        public string FaturaEmail { get; set; }
    }
}
