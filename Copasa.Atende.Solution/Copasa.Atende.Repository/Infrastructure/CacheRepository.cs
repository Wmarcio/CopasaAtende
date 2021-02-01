using System;
using System.Web;
using System.Web.Caching;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Repositório Cache.
    /// </summary>
    public abstract class CacheRepository<T> : ICacheRepository<T> where T : class
    {
        private DateTime _expiration;

        /// <summary>
        /// Construtor
        /// </summary>
        public CacheRepository(DateTime expiration)
        {
            _expiration = expiration;
        }

        /// <summary>
        /// Adiciona objeto ao cache
        /// </summary>
        public virtual void SetValue(string key,T value)
        {
            HttpContext.Current.Cache.Add(key, value, null, _expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Busca objeto do cache
        /// </summary>
        public virtual T GetValue(string key)
        {
            return (T)HttpContext.Current.Cache[key];
        }
    }
}
