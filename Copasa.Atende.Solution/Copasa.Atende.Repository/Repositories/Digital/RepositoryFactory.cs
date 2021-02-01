using Copasa.Atende.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories.Digital
{
    /// <summary>
    /// 
    /// </summary>
   public static class RepositoryFactory
    {
        /// <summary>
        /// Propriedade UnitOfWork.
        /// </summary>
        public static IUnitOfWork UnitOfWork
        {
            get { return new UnitOfWork.UnitOfWork(); }
        }
    }
}
