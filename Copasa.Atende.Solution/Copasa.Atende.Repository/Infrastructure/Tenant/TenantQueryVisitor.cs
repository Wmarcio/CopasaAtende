using Copasa.Util.Attributes;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Copasa.Atende.Repository.Infrastructure.Tenant
{
    /// <summary>
    /// Obtemos a consulta inicial e armazená-la na variável currentExpressionBinding.
    /// Depois de fazer isso, criamos outra expressão que o equivalente SQL
    /// é "originalQuery AND TenantId = @TenantIdParameter"
    /// Visitor pattern - classe de implementação que adiciona filtragem para a coluna tenantId, se aplicável
    /// </summary>
    public class TenantQueryVisitor : DefaultExpressionVisitor
    {
        /// <summary>
        /// Sinalizador impede a aplicação da filtragem personalizada duas vezes por consulta
        /// </summary>
        private bool _injectedDynamicFilter;

        /// <summary>
        /// Esse método é chamado antes do que está abaixo quando uma filtragem já existe na consulta (por exemplo, buscar uma entidade por id)
        /// então aplicamos a filtragem dinâmica a este nível
        /// </summary>
        public override DbExpression Visit(DbFilterExpression expression)
        {
            var column = TenantAwareAttribute.GetTenantColumnName(expression.Input.Variable.ResultType.EdmType);
            if (!_injectedDynamicFilter && !string.IsNullOrEmpty(column))
            {
                var newFilterExpression = BuildFilterExpression(expression.Input, expression.Predicate, column);
                if (newFilterExpression != null)
                {
                    // Se não for nulo, um novo DbFilterExpression foi criado com nossos filtros dinâmicos.
                    return base.Visit(newFilterExpression);
                }

            }
            return base.Visit(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = TenantAwareAttribute.GetTenantColumnName(expression.Target.ElementType);
            if (!_injectedDynamicFilter && !string.IsNullOrEmpty(column))
            {
                // Obter a expressão atual
                var dbExpression = base.Visit(expression);
                // Obter a ligação de expressão atual
                var currentExpressionBinding = DbExpressionBuilder.Bind(dbExpression);
                var newFilterExpression = BuildFilterExpression(currentExpressionBinding, null, column);
                if (newFilterExpression != null)
                {
                    //  Se não for nulo, um novo DbFilterExpression foi criado com nossos filtros dinâmicos.
                    return base.Visit(newFilterExpression);
                }
            }

            return base.Visit(expression);
        }

        /// <summary>
        /// Método auxiliar criando a expressão de filtro correta com base nos parâmetros fornecidos
        /// </summary>
        private DbFilterExpression BuildFilterExpression(DbExpressionBinding binding, DbExpression predicate, string column)
        {
            _injectedDynamicFilter = true;

            var variableReference = DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName);
            // Crie a propriedade com base na variável para aplicar a igualdade
            var tenantProperty = DbExpressionBuilder.Property(variableReference, column);

            //Cria o parâmetro que é uma representação de objeto de um parâmetro sql.
            // Temos que criar um parâmetro e não realizar uma comparação direta com a função Equal, por exemplo
            // como essa lógica é armazenada em cache por consulta e chamada apenas uma vez
            var tenantParameter = DbExpressionBuilder.Parameter(tenantProperty.Property.TypeUsage,
                TenantAwareAttribute.TenantIdFilterParameterName);
            // Aplique a igualdade entre propriedade e parâmetro.
            DbExpression newPredicate = DbExpressionBuilder.Equal(tenantProperty, tenantParameter);

            // Se existir um predicado existente (normalmente quando chamado de DbFilterExpression), execute um AND lógico para obter o resultado
            if (predicate != null)
                newPredicate = newPredicate.And(predicate);

            return DbExpressionBuilder.Filter(binding, newPredicate);
        }

    }
}
