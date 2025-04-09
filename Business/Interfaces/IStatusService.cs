using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<ServiceResult<IEnumerable<Status>>> GetAllStatusesAsync();
       
        Task<ServiceResult<Status>> GetStatusByIdAsync(int id);
        Task<ServiceResult<Status>> GetStatusByNameAsync(string statusName);
    }
}