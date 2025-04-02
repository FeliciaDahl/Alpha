using Business.Models;
using Domain.Dto;

namespace Business.Services
{
    public interface IClientService
    {
        Task<ServiceResult<bool>> CreateClientAsync(ClientRegistrationForm form);
        Task<ServiceResult<bool>> DeleteClientAsync(int id);
        Task<ServiceResult<bool>> EditClientAsync(int id, ClientRegistrationForm form);
        Task<ClientResult> GetClientsAsync();
    }
}