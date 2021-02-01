using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CRBXRepository - Envio de baixas de ordens de serviços
    /// </summary>
    public class ISSCN4CRBXRepository : ISRepository<SCN4CRBXReceive>, IISSCN4CRBXRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4CRBXRepository(ILog log)
         : base("EnvioBaixasSICOM:SCN4CRBX_WSD/EnvioBaixasSICOM_SCN4CRBX_WSD_Port", "SCN4CRBX",log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS envio de baixas de ordens de serviços";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CRBXReceive baseModelReceive)
        {            
            foreach(SCN4CRBXReceiveServicos os in baseModelReceive.loteServicosSicom)
            {
                if (os.numeroOrdemServico != null &&  !"".Equals(os.numeroOrdemServico) && !"0".Equals(os.numeroOrdemServico))
                {
                    baseModelReceive.loteServicos.Add(os);
                }

                if (("".Equals(os.dataBaixaSS) || "0".Equals(os.dataBaixaSS)) && !"".Equals(os.dataBaixaOS))
                {
                    os.dataBaixaSS = os.dataBaixaOS;
                    os.horaBaixaSS = os.dataBaixaOS;
                }
            }
        }
    }
}
