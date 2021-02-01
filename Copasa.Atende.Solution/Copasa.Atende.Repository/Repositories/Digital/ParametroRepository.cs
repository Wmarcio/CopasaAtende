using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Atende.Repository.Interfaces.Digital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ParametroRepository : DigitalBaseRepository<ParametroModel>, IParametroRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public ParametroRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public ParametroRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        { }

    }
}
