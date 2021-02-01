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
   public class DEnviaFaturaEmailSend : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string NumeroFatura { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Empresa { get; set; }
    }
}
