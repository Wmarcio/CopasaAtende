using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISAERepository - Gera alteração de economias
    /// </summary>
    public class ISSCN4ISAERepository : ISRepository<SCN4ISAEReceive>, IISSCN4ISAERepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISAERepository(ILog log)
         : base("GeraAlteracaoEconomias:SCN4ISAE_WSD/GeraAlteracaoEconomias_SCN4ISAE_WSD_Port", "SCN4ISAE", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS gera alteração de economias";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISAEReceive baseModelReceive)
        {
            if (!"".Equals(baseModelReceive.horaPrevisaoSS) && !"0".Equals(baseModelReceive.horaPrevisaoSS))
            {
                baseModelReceive.horaPrevisaoSS = long.Parse(baseModelReceive.horaPrevisaoSS).ToString("00:00");
            }
        }
    }
}
