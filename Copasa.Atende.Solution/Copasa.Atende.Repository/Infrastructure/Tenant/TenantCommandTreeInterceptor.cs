using Copasa.Util.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using static Copasa.Util.TenantUtil;

namespace Copasa.Atende.Repository.Infrastructure.Tenant
{

    /// <summary>
    /// Implementação de  <see cref="IDbCommandTreeInterceptor"/> com filtros baseados em tenantId.
    /// </summary>
    public class TenantCommandTreeInterceptor : IDbCommandTreeInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interceptionContext"></param>
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                // Verifique se há um usuário autenticado neste contexto
                string userId = string.Empty; //Copasa.Util.TenantUtil.IdTenantInSession;

                if (userId == null)
                {
                    userId = Constants.TENANT_INVALIDO;
                }

                // No caso do comando query, altere a consulta adicionando uma filtragem baseada em tenantId
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    var newQuery = queryCommand.Query.Accept(new TenantQueryVisitor());
                    interceptionContext.Result = new DbQueryCommandTree(
                        queryCommand.MetadataWorkspace,
                        queryCommand.DataSpace,
                        newQuery);
                    return;
                }

                if (InterceptInsertCommand(interceptionContext, userId))
                {
                    return;
                }

                if (InterceptUpdate(interceptionContext, userId))
                {
                    return;
                }

                InterceptDeleteCommand(interceptionContext, userId);
            }
        }

        /// <summary>
        /// No caso de um comando insert, sempre atribuímos o valor correto ao tenantId
        /// </summary>
        private static bool InterceptInsertCommand(DbCommandTreeInterceptionContext interceptionContext, string userId)
        {
            var insertCommand = interceptionContext.Result as DbInsertCommandTree;
            if (insertCommand != null)
            {
                var column = TenantAwareAttribute.GetTenantColumnName(insertCommand.Target.VariableType.EdmType);
                if (!string.IsNullOrEmpty(column))
                {
                    // Crie a referência de variável para criar a propriedade
                    var variableReference = DbExpressionBuilder.Variable(insertCommand.Target.VariableType,
                        insertCommand.Target.VariableName);
                    // Crie a propriedade para a qual irá atribuir o valor correto
                    var tenantProperty = DbExpressionBuilder.Property(variableReference, column);
                    // Crie a cláusula set, representação de objeto do comando sql insert
                    var tenantSetClause =
                        DbExpressionBuilder.SetClause(tenantProperty, DbExpression.FromString(userId));

                    // Remover atribuição potencial de tenantId para segurança extra
                    var filteredSetClauses =
                        insertCommand.SetClauses.Cast<DbSetClause>()
                            .Where(sc => ((DbPropertyExpression)sc.Property).Property.Name != column);

                    // Construa as cláusulas finais, representação de objeto dos valores do comando sql insert
                    var finalSetClauses =
                        new ReadOnlyCollection<DbModificationClause>(new List<DbModificationClause>(filteredSetClauses)
                        {
                            tenantSetClause
                        });

                    // Construir o novo comando
                    var newInsertCommand = new DbInsertCommandTree(
                        insertCommand.MetadataWorkspace,
                        insertCommand.DataSpace,
                        insertCommand.Target,
                        finalSetClauses,
                        insertCommand.Returning);

                    interceptionContext.Result = newInsertCommand;
                    // Verdadeiro significa que uma interceptação aconteceu com sucesso, então não há necessidade de continuar
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// No caso de um comando update, filtramos sempre com base no tenantId
        /// </summary>
        private static bool InterceptUpdate(DbCommandTreeInterceptionContext interceptionContext, string userId)
        {
            var updateCommand = interceptionContext.Result as DbUpdateCommandTree;
            if (updateCommand != null)
            {
                var column = TenantAwareAttribute.GetTenantColumnName(updateCommand.Target.VariableType.EdmType);
                if (!string.IsNullOrEmpty(column))
                {
                    // Crie a referência de variável para criar a propriedade
                    var variableReference = DbExpressionBuilder.Variable(updateCommand.Target.VariableType,
                        updateCommand.Target.VariableName);
                    // Crie a propriedade para a qual irá atribuir o valor correto
                    var tenantProperty = DbExpressionBuilder.Property(variableReference, column);
                    // Crie o tenantId onde predicado, representação de objeto de sql onde tenantId = declaração de valor
                    var tenantIdWherePredicate = DbExpressionBuilder.Equal(tenantProperty, DbExpression.FromString(userId));

                    // Remover atribuição potencial de tenantId para segurança extra
                    var filteredSetClauses =
                        updateCommand.SetClauses.Cast<DbSetClause>()
                            .Where(sc => ((DbPropertyExpression)sc.Property).Property.Name != column);

                    // Construa as cláusulas finais, representação de objeto dos valores do comando sql update
                    var finalSetClauses =
                        new ReadOnlyCollection<DbModificationClause>(new List<DbModificationClause>(filteredSetClauses));

                    // O predicado inicial é o sql where statement
                    var initialPredicate = updateCommand.Predicate;
                    // Adicione à instrução inicial a instrução tenantId que se traduz em sql AND TenantId = 'valor'
                    var finalPredicate = initialPredicate.And(tenantIdWherePredicate);

                    var newUpdateCommand = new DbUpdateCommandTree(
                        updateCommand.MetadataWorkspace,
                        updateCommand.DataSpace,
                        updateCommand.Target,
                        finalPredicate,
                        finalSetClauses,
                        updateCommand.Returning);

                    interceptionContext.Result = newUpdateCommand;
                    // Verdadeiro significa que uma interceptação aconteceu com sucesso, então não há necessidade de continuar
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// No caso de um comando delete, filtramos sempre com base no tenantId
        /// </summary>
        private static void InterceptDeleteCommand(DbCommandTreeInterceptionContext interceptionContext, string userId)
        {
            var deleteCommand = interceptionContext.Result as DbDeleteCommandTree;
            if (deleteCommand != null)
            {
                var column = TenantAwareAttribute.GetTenantColumnName(deleteCommand.Target.VariableType.EdmType);
                if (!string.IsNullOrEmpty(column))
                {
                    // Crie a referência de variável para criar a propriedade
                    var variableReference = DbExpressionBuilder.Variable(deleteCommand.Target.VariableType,
                        deleteCommand.Target.VariableName);
                    // Crie a propriedade para a qual irá atribuir o valor correto
                    var tenantProperty = DbExpressionBuilder.Property(variableReference, column);
                    var tenantIdWherePredicate = DbExpressionBuilder.Equal(tenantProperty, DbExpression.FromString(userId));

                    // O predicado inicial é o sql where statement
                    var initialPredicate = deleteCommand.Predicate;
                    // Adicione à instrução inicial a instrução tenantId que se traduz em sql AND TenantId = 'valor'
                    var finalPredicate = initialPredicate.And(tenantIdWherePredicate);

                    var newDeleteCommand = new DbDeleteCommandTree(
                        deleteCommand.MetadataWorkspace,
                        deleteCommand.DataSpace,
                        deleteCommand.Target,
                        finalPredicate);

                    interceptionContext.Result = newDeleteCommand;
                }
            }
        }

    }
}
