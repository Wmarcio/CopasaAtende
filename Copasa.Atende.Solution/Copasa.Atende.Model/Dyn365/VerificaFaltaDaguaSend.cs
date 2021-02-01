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
   public class VerificaFaltaDaguaSend : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NomeLogradouro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NumeroProtocolo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NomeSolicitante { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelefoneSolicitante { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReferenciaEndereço { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Empresa { get; set; }
    }
}
