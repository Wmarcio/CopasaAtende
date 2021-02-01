using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// BrokerSCN3PCLIRepository - Dados básicos cliente
    /// </summary>
    public class BrokerSCN3PCLIRepository : BrokerRepository<SCN3PCLIReceive>, IBrokerSCN3PCLIRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public BrokerSCN3PCLIRepository()
         : base("SCN3PCLI")
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository Broker Dados básicos cliente";
        }
    }
}
