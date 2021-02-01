using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// IDAOMensagensRepository - Mensagens de retorno
    /// </summary>
    public interface IDAOMensagensRepository : IBaseDao
    {
        /// <summary>
        /// Retorno descrição da mensagem
        /// </summary>
        string getDescricaoMensagem(string idMensagem);
    }
}
