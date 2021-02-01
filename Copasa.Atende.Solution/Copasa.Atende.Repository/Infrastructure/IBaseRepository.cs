namespace Copasa.Atende.Repository.Infrastructure
{
    using Model.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface para o Repositório Base.
    /// </summary>
    /// <typeparam name="T">Entidade.</typeparam>
    public interface IBaseRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Método Adicionar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        void Add(T entity);

        /// <summary>
        /// Método Deletar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        void Delete(T entity);

        /// <summary>
        /// Método Deletar Vários.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        void Delete(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Método Atualizar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        void Update(T entity);

        /// <summary>
        /// Obter consulta base com MultiTenant.
        /// </summary>
        /// <returns>Consulta com o MultiTenant.</returns>
        IQueryable<T> GetBaseQuery();

        /// <summary>
        /// Método para obter entidade a partir de uma expressão.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Entidade.</returns>
        T Get(Expression<Func<T, bool>> expression, bool isNoTracking = false);

        /// <summary>
        /// Método para obter entidade a partir de uma expressão.
        /// </summary>
        /// <typeparam name="K">Tipo de dados para ordenação.</typeparam>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação.</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Entidade.</returns>
        T Get<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false);

        /// <summary>
        /// Método para obter todos os registros.
        /// </summary>
        /// <returns>Lista de registros.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Método para obter todos os registros com ordenação.
        /// </summary>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <returns>Lista de registros ordenada.</returns>
        IList<T> GetAll(Expression<Func<T, object>> sortExpression, bool orderByDesc = false);

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (inteiro).</param>
        /// <returns>Entidade.</returns>
        T GetById(int id);

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (string).</param>
        /// <returns>Entidade.</returns>
        T GetById(string id);

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (Guid).</param>
        /// <returns>Entidade.</returns>
        T GetById(Guid id);

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão com ordenação.
        /// </summary>
        /// <typeparam name="K">Tipo de dados para ordenação.</typeparam>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Lista de registros.</returns>
        IList<T> GetMany<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false);

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <returns>Lista de registros.</returns>
        IList<T> GetMany(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Verifica se já existe registro com a expressão dada
        /// </summary>
        /// <param name="expression">expressão</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> expression);
    }
}
