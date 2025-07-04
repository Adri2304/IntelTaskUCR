//using Azure.Core;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IntelTaskUcrContext _dbContext;

        public RequestRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<List<Request>> ReadRequestAsync(int? idRequest)
        {
            var result = new List<Request>();

            if (idRequest.HasValue)
            {
                var request = await _dbContext.TPermisos.FindAsync(idRequest);
                if (request != null)
                {
                    result.Add(new Request(
                        request.CnIdPermiso,
                        request.CtTituloPermiso,
                        request.CtDescripcionPermiso,
                        request.CnIdEstado,
                        request.CtDescripcionRechazo,
                        request.CfFechaHoraRegistro,
                        request.CfFechaHoraInicioPermiso,
                        request.CfFechaHoraFinPermiso,
                        request.CnUsuarioCreador));
                }
                return result;
            }
            var requests = await _dbContext.TPermisos.ToListAsync();

            foreach (var request in requests)
            {
                result.Add(new Request(
                        request.CnIdPermiso,
                        request.CtTituloPermiso,
                        request.CtDescripcionPermiso,
                        request.CnIdEstado,
                        request.CtDescripcionRechazo,
                        request.CfFechaHoraRegistro,
                        request.CfFechaHoraInicioPermiso,
                        request.CfFechaHoraFinPermiso,
                        request.CnUsuarioCreador));
            }
            return result;
        }

        public async Task<bool> CreateRequestAsync(Dictionary<string, object?> data)
        {
            var request = new TPermiso();
            var type = request.GetType();

            foreach (var item in data)
            {
                var property = type.GetProperty(item.Key);
                property?.SetValue(request, item.Value);
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    var id = await _dbContext.TPermisos.
                    Select(x => x.CnIdPermiso)
                    .OrderByDescending(x => x)
                    .FirstOrDefaultAsync();

                    request.CnIdPermiso = id + 1;
                    await _dbContext.TPermisos.AddAsync(request);
                    var result = await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result == 1;

                } catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<List<Request>> ReadRequestPerUserAsync(int idUser)
        {
            var result = new List<Request>();

            var requests = await _dbContext.TPermisos
                .Where(x => x.CnUsuarioCreador == idUser)
                .ToListAsync();

            foreach (var request in requests)
            {
                result.Add(new Request(
                    request.CnIdPermiso,
                    request.CtTituloPermiso,
                    request.CtDescripcionPermiso,
                    request.CnIdEstado,
                    request.CtDescripcionRechazo,
                    request.CfFechaHoraRegistro,
                    request.CfFechaHoraInicioPermiso,
                    request.CfFechaHoraFinPermiso,
                    request.CnUsuarioCreador));
            }
            return result;
        }

        public async Task<bool> UpdateRequestAsync(Dictionary<string, object?> data, int idRequest)
        {
            var request = await _dbContext.TPermisos.FindAsync(idRequest);

            if (request == null)
                return false;

            var type = request.GetType();

            foreach (var item in data)
            {
                var property = type.GetProperty(item.Key);
                property?.SetValue(request, item.Value);
            }

            _dbContext.Entry(request).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}
