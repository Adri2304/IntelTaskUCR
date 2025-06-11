using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.InteropServices;

namespace IntelTaskUCR.API.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        public RequestService(IRequestRepository requestRepository) => _requestRepository = requestRepository;

        public async Task<List<Request>> ReadRequestAsync(int? idRequest)
        {
            return await _requestRepository.ReadRequestAsync(idRequest);
        }

        public async Task<bool> CreateRequestAsync(Dictionary<string, object?> data)
        {
            return await _requestRepository.CreateRequestAsync(data);
        }

        public async Task<List<Request>> ReadRequestPerUserAsync(int idUser)
        {
            return await _requestRepository.ReadRequestPerUserAsync(idUser);
        }

        public async Task<bool> UpdateRequestAsync(Dictionary<string, object?> data, int idRequest)
        {
            return await _requestRepository.UpdateRequestAsync(data, idRequest);
        }

        
    }
}
