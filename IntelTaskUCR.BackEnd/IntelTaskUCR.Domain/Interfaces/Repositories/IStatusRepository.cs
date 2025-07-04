using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<object>> ReadStatesAsync();
        Task<IEnumerable<object>> ReadComplexitiesAsync();
        Task<IEnumerable<object>> ReadPrioritiesAsync();
    }
}
