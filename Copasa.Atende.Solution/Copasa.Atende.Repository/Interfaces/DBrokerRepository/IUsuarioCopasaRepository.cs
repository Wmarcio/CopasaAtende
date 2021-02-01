using Copasa.Atende.Model.Broker;
using Copasa.Atende.Model.Enumerador;
using Copasa.Atende.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Interfaces.DBrokerRepository
{
    /// <summary>
    /// Interface IUsuarioCopasaRepository
    /// </summary>
    public interface IUsuarioCopasaRepository : IDBrokerRepository<UsuarioCopasaModel>
    {
        /// <summary>
        /// Atualiza usuário Copasa, apenas e-mail e telefones
        /// </summary>
        /// <param name="usuarioCopasa">Objeto de entrada.</param>
        /// <param name="ambiente">Ambiente.</param>
        /// <returns></returns>
        UsuarioCopasaReceive AtualizacaoCadastral(UsuarioCopasaModel usuarioCopasa, EnvironmentEnum ambiente);
    }
}
