using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// BrokerSCN6UEFIRepository - Informa identificador e retorna matriculas
    /// </summary>
    public class BrokerSCN6UEFIRepository : BrokerRepository<SCN6UEFIReceive>, IBrokerSCN6UEFIRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public BrokerSCN6UEFIRepository()
         : base("SCN6UEFI")
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository Broker Informa identificador e retorna matriculas";
        }
    }
}
