using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class RejectJustifyingRepository : IRejectJustifyingRepository
    {
        private readonly IntelTaskUcrContext _dbContext;

        public RejectJustifyingRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<bool> CreateRejectJustifyingAsync(int idTask, string message)
        {
            var rejectJustifying = new TTareasJustificacionRechazo();
            rejectJustifying.CtDescripcionRechazo = message;
            rejectJustifying.CnIdTarea = idTask;
            rejectJustifying.CfFechaHoraRechazo = DateTime.Now;

            using (var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    var id = await _dbContext.TTareasJustificacionRechazos.
                    Select(x => x.CnIdTareaRechazo)
                    .OrderByDescending(x => x)
                    .FirstOrDefaultAsync();

                    rejectJustifying.CnIdTareaRechazo = id + 1;
                    await _dbContext.TTareasJustificacionRechazos.AddAsync(rejectJustifying);
                    var result = await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result == 1;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }

        }
    }
}
