using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Repositório Religação
    /// </summary>
    public class ISSCN4ISRERepository : ISRepository<SCN4ISREReceive>, IISSCN4ISRERepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ISSCN4ISRERepository(ILog log)
            :base("Religacao:SCN4ISRE_WSD/Religacao_SCN4ISRE_WSD_Port", "SCN4ISRE", log)
        {

        }

        /// <summary>
        /// Nome da entidade
        /// </summary>
        /// <returns></returns>
        public override string GetEntidadeNome()
        {
            return "Repository IS religação";
        }

        /// <summary>
        /// Trata retorno do Sicom
        /// </summary>
        /// <param name="baseModelReceive"></param>
        protected override void TratarRetorno(SCN4ISREReceive baseModelReceive)
        {
            
        }
    }
}
