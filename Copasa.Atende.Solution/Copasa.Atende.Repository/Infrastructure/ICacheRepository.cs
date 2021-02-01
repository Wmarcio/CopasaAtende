namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface para o Repositório Cache.
    /// </summary>
    public interface ICacheRepository<T> where T : class
    {

        /// <summary>
        /// Adiciona objeto ao cache
        /// </summary>
        void SetValue(string key, T value);

        /// <summary>
        /// Busca objeto do cache
        /// </summary>
        T GetValue(string key);
    }
}
