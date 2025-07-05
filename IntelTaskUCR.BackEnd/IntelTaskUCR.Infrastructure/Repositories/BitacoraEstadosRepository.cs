using IntelTaskUCR.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelTaskUCR.Domain.Interfaces.Repositories;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class BitacoraEstadosRepository : IBitacoraEstadosRepository
    {
        private readonly IntelTaskUcrContext _dbContext;

        public BitacoraEstadosRepository(IntelTaskUcrContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> createAsync(int idTask, int idOldState, int idNewState, int idUser)
        {
            await _dbContext.TBitacoraCambiosEstados.AddAsync(new TBitacoraCambiosEstado
            {
                CnIdTareaPermiso = idTask,
                CnIdTipoDocumento = 1,
                CnIdEstadoAnterior = (byte)idOldState,
                CnIdEstadoNuevo = (byte)idNewState,
                CfFechaHoraCambio = DateTime.Now,
                CnIdUsuarioResponsable = idUser
            });

            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}
