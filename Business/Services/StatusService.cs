
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public Task<ServiceResult<IEnumerable<Status>>> GetAllClientsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<IEnumerable<Status>>> GetAllStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        var status = result.Result?.Select(c => c.MapTo<Status>());

        return ServiceResult<IEnumerable<Status>>.Success(status!);
    }

    public async Task<ServiceResult<Status>> GetStatusByIdAsync(int id)
    {
        var result = await _statusRepository.GetAsync(where: x => x.Id == id);

        var status = result.Result?.MapTo<Status>();
        return result.Succeeded

            ? ServiceResult<Status>.Success(status!)
            : ServiceResult<Status>.Failed(result.StatusCode, "Status not found");
    }

    public async Task<ServiceResult<Status>> GetStatusByNameAsync(string statusName)
    {
        var result = await _statusRepository.GetAsync(where: x => x.StatusName == statusName);

        var status = result.Result?.MapTo<Status>();
        return result.Succeeded
            ? ServiceResult<Status>.Success(status!)
            : ServiceResult<Status>.Failed(result.StatusCode, "Status not found");
    }
}
