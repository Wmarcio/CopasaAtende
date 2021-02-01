using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN7ISOPRepository - Onde pagar a conta
    /// </summary>
    public class ISSCN7ISOPRepository : ISRepository<SCN7ISOPReceive>, IISSCN7ISOPRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN7ISOPRepository(ILog log)
         : base("OndePagarConta:SCN7ISOP_WSD/OndePagarConta_SCN7ISOP_WSD_Port", "SCN7ISOP", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS onde pagar a conta";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN7ISOPReceive baseModelReceive)
        {
            foreach (SCN7ISOPReceiveLocal local in baseModelReceive.locaisSicom)
            {
                if (local.estabelecimento != null && !"".Equals(local.estabelecimento))
                {
                    baseModelReceive.locais.Add(local);
                }
            }
        }
    }
}
