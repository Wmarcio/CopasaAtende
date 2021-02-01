using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository
{
    /// <summary>
    /// ISSCN6ISFPRepository - Lista faturas pagas
    /// </summary>
    public class ISSCN6ISFPRepository : ISRepository<SCN6ISFPReceive>, IISSCN6ISFPRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISFPRepository(ILog log)
         : base("FaturasPagas:SCN6ISFP_WSD/FaturasPagas_SCN6ISFP_WSD_Port", "SCN6ISFP", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS lista faturas pagas";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISFPReceive baseModelReceive)
        {
            foreach (SCN6ISFPReceiveContas conta in baseModelReceive.contasSicom)
            {
                if (conta.referencia != null && !"".Equals(conta.referencia))
                {
                    baseModelReceive.contas.Add(conta);
                }
            }
        }
    }
}
