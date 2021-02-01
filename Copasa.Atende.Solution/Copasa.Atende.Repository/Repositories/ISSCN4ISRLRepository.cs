using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISRLRepository - Religação
    /// </summary>
    public class ISSCN4ISRLRepository : ISRepository<SCN4ISRLReceive>, IISSCN4ISRLRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISRLRepository(ILog log)
         : base("Religacao:SCN4ISRL_WSD/Religacao_SCN4ISRL_WSD_Port", "SCN4ISRL", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS religação";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISRLReceive baseModelReceive)
        {
            if ("".Equals(baseModelReceive.valorReligacao))
                baseModelReceive.valorReligacao = "0,00";
            if ("".Equals(baseModelReceive.valorServico1))
                baseModelReceive.valorServico1 = "0,00";
            if ("".Equals(baseModelReceive.valorServico2))
                baseModelReceive.valorServico2 = "0,00";
        }
    }
}
