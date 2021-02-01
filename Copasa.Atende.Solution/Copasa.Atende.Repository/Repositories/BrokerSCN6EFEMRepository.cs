using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// BrokerSCN6EFEMRepository - Segunda via de fatura
    /// </summary>
    public class BrokerSCN6EFEMRepository : BrokerRepository<SCN6EFEMReceive>, IBrokerSCN6EFEMRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public BrokerSCN6EFEMRepository()
         : base("SCN6EFEM")
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository Broker Segunda via de fatura";
        }
    }
}
