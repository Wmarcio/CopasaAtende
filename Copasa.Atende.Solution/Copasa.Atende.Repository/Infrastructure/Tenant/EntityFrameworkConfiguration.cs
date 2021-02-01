using System.Data.Entity;

namespace Copasa.Atende.Repository.Infrastructure.Tenant
{ /// <summary>
  /// Esta classe pode fazer várias configurações e; Ela é descoberta automaticamente pelo Entity Framework
  /// Importante: Apenas uma instância de uma classe derivada de DbConfiguration deve existir por AppDomain.
  /// </summary>
    public class EntityFrameworkConfiguration : DbConfiguration
    {
        /// <summary>
        /// Conctrutora com os novos inteceptadores
        /// </summary>
        public EntityFrameworkConfiguration()
        {
            //AddInterceptor(new TenantCommandInterceptor());
            //AddInterceptor(new TenantCommandTreeInterceptor());
        }
    }
}
