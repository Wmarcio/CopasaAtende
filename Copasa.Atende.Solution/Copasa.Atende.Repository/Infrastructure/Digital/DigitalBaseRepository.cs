﻿using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Infrastructure.Digital
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DigitalBaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseModel
    {
        private CopasaDigitalDataContext _dbContext = new CopasaDigitalDataContext();
        private bool _isCommit;

        /// <summary>
        /// Log do log4Net
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Construtor vazio.
        /// </summary>
        public DigitalBaseRepository()
        {
            _isCommit = true;
            _dbContext.Database.Log = (dbLog => log.Debug(dbLog));
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbContext">AppDataContext</param>
        // TODO Trocar para o DbContext do seu projeto.
        public DigitalBaseRepository(CopasaDigitalDataContext dbContext)
        {
            _dbContext = dbContext;
            _isCommit = false;
            _dbContext.Database.Log = (dbLog => log.Debug(dbLog));
        }


        /// <summary>
        /// Propriedade Database
        /// </summary>
        internal Database Database
        {
            get { return _dbContext.Database; }
        }

        /// <summary>
        /// Propriedade AppDataContext
        /// </summary>
        // TODO Trocar para o DbContext do seu projeto.
        internal CopasaDigitalDataContext AppDataContext
        {
            get { return _dbContext; }
        }


        /// <summary>
        /// Método para obter entidade a partir de uma expressão.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Entidade.</returns>
        public T Get(Expression<Func<T, bool>> expression, bool isNoTracking = false)
        {
            var query = GetBaseQuery();

            if (!isNoTracking)
            {
                return query.Where(expression).FirstOrDefault();
            }
            else
            {
                return query.AsNoTracking().Where(expression).FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para obter entidade a partir de uma expressão.
        /// </summary>
        /// <typeparam name="K">Tipo de dados para ordenação.</typeparam>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação.</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Entidade.</returns>
        public T Get<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false)
        {
            var query = GetBaseQuery();

            if (!isNoTracking)
            {
                return !orderByDesc ? query.Where(expression).OrderBy(sortExpression).FirstOrDefault()
                    : query.Where(expression).OrderByDescending(sortExpression).FirstOrDefault();
            }
            else
            {
                return !orderByDesc ? query.AsNoTracking().Where(expression).OrderBy(sortExpression).FirstOrDefault()
                    : query.AsNoTracking().Where(expression).OrderByDescending(sortExpression).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obter consulta base com MultiTenant.
        /// </summary>
        /// <returns>Consulta com o MultiTenant.</returns>
        public IQueryable<T> GetBaseQuery()
        {
            T obj = Activator.CreateInstance<T>();
            return _dbContext.Set<T>().AsQueryable();
        }

        /// <summary>
        /// Método Dispose.
        /// </summary>
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }

            GC.SuppressFinalize(this);
        }


        #region metodos obrigatorios
        /// <summary>
        /// Método Adicionar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        public virtual void Add(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                if (_isCommit) _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método Deletar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        public virtual void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(entity);
            if (_isCommit) _dbContext.SaveChanges();
        }

        /// <summary>
        /// Método Deletar Vários.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        public void Delete(Expression<Func<T, bool>> expression)
        {
            var objects = GetMany(expression);

            foreach (T obj in objects)
            {
                _dbContext.Set<T>().Remove(obj);
            }

            if (_isCommit) _dbContext.SaveChanges();
        }

        /// <summary>
        /// Método Atualizar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (_isCommit) _dbContext.SaveChanges();
        }

        /// <summary>
        /// Método para obter todos os registros.
        /// </summary>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetAll()
        {
            return GetBaseQuery().ToList();
        }

        /// <summary>
        /// Método para obter todos os registros com ordenação.
        /// </summary>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <returns>Lista de registros ordenada.</returns>
        public IList<T> GetAll(Expression<Func<T, object>> sortExpression, bool orderByDesc = false)
        {
            var query = GetBaseQuery();

            return !orderByDesc ? query.OrderBy(sortExpression).ToList()
                : query.OrderByDescending(sortExpression).ToList();
        }

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (inteiro).</param>
        /// <returns>Entidade.</returns>
        public T GetById(int id)
        {
            var obj = _dbContext.Set<T>().Find(id);
            return obj;
        }

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (string).</param>
        /// <returns>Entidade.</returns>
        public T GetById(string id)
        {
            var obj = _dbContext.Set<T>().Find(id);
            return obj;
        }

        /// <summary>
        /// Método obter por Id.
        /// </summary>
        /// <param name="id">Id da entidade (Guid).</param>
        /// <returns>Entidade.</returns>
        public T GetById(Guid id)
        {
            var obj = _dbContext.Set<T>().Find(id);
            return obj;
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão com ordenação.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false)
        {
            var query = GetBaseQuery();

            if (!isNoTracking)
            {
                return !orderByDesc ? query.Where(expression).OrderBy(sortExpression).ToList()
                    : query.Where(expression).OrderByDescending(sortExpression).ToList();
            }
            else
            {
                return !orderByDesc ? query.AsNoTracking().Where(expression).OrderBy(sortExpression).ToList()
                    : query.AsNoTracking().Where(expression).OrderByDescending(sortExpression).ToList();
            }
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão.
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany(Expression<Func<T, bool>> expression)
        {
            var query = GetBaseQuery();
            return query.Where(expression).ToList();
        }

        /// <summary>
        /// Verifica se já existe registro com a expressão dada
        /// </summary>
        /// <param name="expression">expressão</param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            bool exist = Get(expression, true) != null;

            return exist;
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão e includes
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="includes">Coleção de Includes</param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany(Expression<Func<T, bool>> expression, params string[] includes)
        {

            IQueryable<T> q = _dbContext.Set<T>();

            foreach (var include in includes)
            {
                q = q.Include(include);
            }

            return q.Where(expression).ToList();
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão e includes
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="includes">Coleção de Includes</param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> q = _dbContext.Set<T>();

            if (includes == null || includes.Length <= 0)
                return q.Where(expression).ToList();

            foreach (var include in includes)
            {
                q = q.Include(include);
            }

            return q.Where(expression).ToList();
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão e includes
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <param name="includes">Coleção de Includes</param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false, params string[] includes)
        {

            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (!isNoTracking)
            {
                return !orderByDesc ? query.Where(expression).OrderBy(sortExpression).ToList()
                    : query.Where(expression).OrderByDescending(sortExpression).ToList();
            }
            else
            {
                return !orderByDesc ? query.AsNoTracking().Where(expression).OrderBy(sortExpression).ToList()
                    : query.AsNoTracking().Where(expression).OrderByDescending(sortExpression).ToList();
            }
        }

        /// <summary>
        /// Método para obter uma lista de entidade a partir de uma expressão e includes
        /// </summary>
        /// <param name="expression">Expressão lambda.</param>
        /// <param name="sortExpression">Expressão lambda com ordenação</param>
        /// <param name="orderByDesc">Parâmetro enviado quando a ordenação é descresente.</param>
        /// <param name="isNoTracking"></param>
        /// <param name="includes">Coleção de Includes</param>
        /// <returns>Lista de registros.</returns>
        public IList<T> GetMany<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, bool orderByDesc = false, bool isNoTracking = false, params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (!isNoTracking)
            {
                return !orderByDesc ? query.Where(expression).OrderBy(sortExpression).ToList()
                    : query.Where(expression).OrderByDescending(sortExpression).ToList();
            }
            else
            {
                return !orderByDesc ? query.AsNoTracking().Where(expression).OrderBy(sortExpression).ToList()
                    : query.AsNoTracking().Where(expression).OrderByDescending(sortExpression).ToList();
            }
        }
        #endregion 

    }
}