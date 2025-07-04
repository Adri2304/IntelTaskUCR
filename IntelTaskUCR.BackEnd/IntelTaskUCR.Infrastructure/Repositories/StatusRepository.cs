using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IntelTaskUcrContext _dbContext;
        public StatusRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<object>> ReadStatesAsync()
        {
            return await _dbContext.TEstados
                .Select(x => new {x.CnIdEstado, x.CtEstado})
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> ReadComplexitiesAsync()
        {
            return await _dbContext.TComplejidades
                .Select(x => new {x.CnIdComplejidad, x.CtNombre})
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> ReadPrioritiesAsync()
        {
            return await _dbContext.TPrioridades
                .Select(x => new { x.CnIdPrioridad, x.CtNombrePrioridad})
                .ToListAsync();
        }
    }
}
