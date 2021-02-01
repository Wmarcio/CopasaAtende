using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Repositório SCN4CASS Cancelamento Serviço
    /// </summary>
    public class ISSCN4CASSRepository : ISRepository<SCN4CASSReceive>, IISSCN4CASSRepository
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ISSCN4CASSRepository(ILog log)
            :base("CancelamentoServico:SCN4CASS_WSD/CancelamentoServico_SCN4CASS_WSD_Port", "SCN4CASS",log)
        {

        }
        /// <summary>
        /// Retorna o nome da entidade
        /// </summary>
        /// <returns></returns>
        public override string GetEntidadeNome()
        {
            return "Repository IS Cancelamento de Serviço";
        }

        /// <summary>
        /// Trata retorno do Sicom
        /// </summary>
        /// <param name="baseModelReceive"></param>
        protected override void TratarRetorno(SCN4CASSReceive baseModelReceive)
        {
            
        }
    }
}
