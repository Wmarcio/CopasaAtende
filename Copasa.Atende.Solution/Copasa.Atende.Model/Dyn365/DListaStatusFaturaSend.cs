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
    public class DListaStatusFaturaSend : BaseModel
    {

        /// <summary>
        /// Identificador do CPF/CNPJ.
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// CPF/CNPJ.
        /// </summary>
        public string CpfCnpj { get; set; }
    }
}
