using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Interfaces.Dyn365
{
    /// <summary>
    /// Define o serviço e objeto que serão usados no dynamic repository
    /// </summary>
    public interface IDRepository : IDynamicsRepository<BaseModel>
    {

    }
}
