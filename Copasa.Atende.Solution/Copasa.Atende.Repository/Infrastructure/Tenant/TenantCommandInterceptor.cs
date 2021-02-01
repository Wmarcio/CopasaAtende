using Copasa.Util.Attributes;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using static Copasa.Util.TenantUtil;

namespace Copasa.Atende.Repository.Infrastructure.Tenant
{
    /// <summary>
    /// Implementação de <see cref="IDbCommandInterceptor"/>.
    /// Nesta classe, definimos o valor real de tenantId ao consultar o banco de dados à medida que a árvore de comandos é armazenada em cache
    /// </summary>
    internal class TenantCommandInterceptor : IDbCommandInterceptor
    {

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        private static void SetTenantParameterValue(DbCommand command)
        {
            /* var idTenant = Constants.TENANT_INVALIDO;
             if ((command == null) || (command.Parameters.Count == 0) || idTenant == null)
             {
                 // Se não houver um tenant em uma uma classe marcada como tenant, o sistema atribuira um tenant inexistente para que 
                 // a visibilidade dos dados permaneça inalterada.
                 idTenant = Constants.TENANT_INVALIDO;
             }


             if (idTenant != null)
             {
                 // Enumerar todos os parâmetros de comando e atribuir o valor correto no que adicionamos dentro do  query visitor
                 foreach (DbParameter param in command.Parameters)
                 {
                     if (param.ParameterName != TenantAwareAttribute.TenantIdFilterParameterName)
                         continue;
                     param.Value = idTenant;
                 }
             }*/
        }
    }
}
