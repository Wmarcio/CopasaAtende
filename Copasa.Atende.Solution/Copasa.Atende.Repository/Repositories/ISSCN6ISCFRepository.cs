using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISCFRepository - Simula fatura
    /// </summary>
    public class ISSCN6ISCFRepository : ISRepository<SCN6ISCFReceive>, IISSCN6ISCFRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISCFRepository(ILog log)
         : base("SimulaFatura:SCN6ISCF_WSD/SimulaFatura_SCN6ISCF_WSD_Port", "SCN6ISCF", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS simula fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISCFReceive baseModelReceive)
        {
        }
    }
}
