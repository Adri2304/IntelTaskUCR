using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface IBitacoraEstadosRepository
    {
        Task<bool> createAsync(int idTask, int idOldState, int idNewState, int idUser);
    }
}
