using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISVIRepository - Vazamento no imovel
    /// </summary>
    public class ISSCN4ISVIRepository : ISRepository<SCN4ISVIReceive>, IISSCN4ISVIRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISVIRepository(ILog log)
         : base("InformarLeitura:SCN4ISFA_WSD/VazamentoImovel_SCN4ISVI_WSD_Port", "SCN4ISVI", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS vazamento no imovel";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISVIReceive baseModelReceive)
        {
        }
    }
}
