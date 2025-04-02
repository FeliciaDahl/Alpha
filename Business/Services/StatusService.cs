
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Extensions;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult> GetStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        return result.MapTo<StatusResult>();

    }
}
