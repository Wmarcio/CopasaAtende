using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// IORAEmpregadoRepository - Tabela de empregados no Oracle
    /// </summary>
    public interface IORAEmpregadoRepository : IBaseRepository<ORAEmpregado>
    {
        /// <summary>
        /// Preenche dados do usuário de acordo com seu perfil
        /// </summary>
        bool preencheDadosUsuario(TrabUsuario trabUsuario);

    }
}
