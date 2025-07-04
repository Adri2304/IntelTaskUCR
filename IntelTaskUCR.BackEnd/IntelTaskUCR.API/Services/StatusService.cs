using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository) => _statusRepository = statusRepository;

        public async Task<IEnumerable<object>> ReadStatesAsync()
        {
            return await _statusRepository.ReadStatesAsync();
        }

        public async Task<IEnumerable<object>> ReadComplexitiesAsync()
        {
            return await _statusRepository.ReadComplexitiesAsync();
        }

        public async Task<IEnumerable<object>> ReadPrioritiesAsync()
        {
            return await _statusRepository.ReadPrioritiesAsync();
        }
    }
}
