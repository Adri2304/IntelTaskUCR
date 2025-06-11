using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.Domain.Interfaces.Services
{
    public interface IRequestService
    {
        Task<List<Request>> ReadRequestAsync(int? idRequest);
        Task<bool> CreateRequestAsync(Dictionary<string, object?> data);
        Task<List<Request>> ReadRequestPerUserAsync(int idUser);
        Task<bool> UpdateRequestAsync(Dictionary<string, object?> data, int idRequest);
    }
}
