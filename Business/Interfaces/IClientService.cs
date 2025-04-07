using Business.Models;
using Domain.Dto;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IClientService
    {
        Task<ServiceResult<Client>> CreateClientAsync(ClientRegistrationForm form);
        Task<ServiceResult<bool>> DeleteClientAsync(int id);
        Task<ServiceResult<bool>> EditClientAsync(int id, ClientEditForm form);
        Task<ServiceResult<Client>> GetClientAsync(int id);
        Task<ClientResult> GetAllClientsAsync();
    }
}