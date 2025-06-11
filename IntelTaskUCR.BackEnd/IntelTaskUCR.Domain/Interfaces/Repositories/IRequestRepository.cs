using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface IRequestRepository
    {
        Task<List<Request>> ReadRequestAsync(int? idRequest);
        Task<bool> CreateRequestAsync(Dictionary<string, object?> data);
        Task<List<Request>> ReadRequestPerUserAsync(int idUser);
        Task<bool> UpdateRequestAsync(Dictionary<string, object?> data, int idRequest);
        //Task ReadRequestPerUserAsync(Dictionary<string, object> newRequest);
        //Task UpdateRequest(int idRequest, Dictionary<string, object> newData);

        //Task ChangeStateAsync(int? idRequest);
    }
}
