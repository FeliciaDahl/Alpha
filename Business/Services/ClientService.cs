
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using System.Diagnostics;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;


    public async Task<ServiceResult<Client>> CreateClientAsync(ClientRegistrationForm form)
    {
        
        if (string.IsNullOrWhiteSpace(form.ClientName))
            return ServiceResult<Client>.Failed(400, "Client name can not be empty");
        
        var clientExist = await _clientRepository.ExistsAsync(c => c.Email == form.Email);

        if (clientExist.Succeeded && clientExist.Result)
            return ServiceResult<Client>.Failed(400, "Client with this email already exist");

        await _clientRepository.BeginTransactionAsync();

        try
        {
            var clientEntity = form.MapTo<ClientEntity>();
            await _clientRepository.AddAsync(clientEntity);
            await _clientRepository.SaveAsync();

            await _clientRepository.CommitTransactionAsync();

            var client = clientEntity.MapTo<Client>();
            return ServiceResult<Client>.Success(client);
        }
        catch (Exception e)
        {
            await _clientRepository.RollbackTransactionAsync();
            return ServiceResult<Client>.Failed(500, e.Message);
        }
    }

    public async Task<ServiceResult<bool>> EditClientAsync(int id, ClientEditForm form)
    {

        var existingClientResult = await _clientRepository.GetEntityAsync(c => c.Id == id);
      
        if (!existingClientResult.Succeeded || existingClientResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Client not found");

        var clientEntity = existingClientResult.Result;

        clientEntity.ClientName = string.IsNullOrWhiteSpace(form.ClientName) ? clientEntity.ClientName : form.ClientName;
        clientEntity.ContactPerson = string.IsNullOrWhiteSpace(form.ContactPerson) ? clientEntity.ContactPerson : form.ContactPerson;
        clientEntity.Email = string.IsNullOrWhiteSpace(form.Email) ? clientEntity.Email : form.Email;
        clientEntity.Location = string.IsNullOrWhiteSpace(form.Location) ? clientEntity.Location : form.Location;
        clientEntity.Phone = string.IsNullOrWhiteSpace(form.Phone) ? clientEntity.Phone : form.Phone;

        await _clientRepository.BeginTransactionAsync();

        try
        {
            await _clientRepository.UpdateAsync(clientEntity);
            await _clientRepository.SaveAsync();
            await _clientRepository.CommitTransactionAsync();

           

            return ServiceResult<bool>.Success(true);

        }
        catch (Exception e)
        {
            await _clientRepository.RollbackTransactionAsync();
            return ServiceResult<bool>.Failed(500, e.Message);

        }
    }

    public async Task<ServiceResult<bool>> DeleteClientAsync(int id)
    {
        var existingClientResult = await _clientRepository.GetEntityAsync(c => c.Id == id);

        if (!existingClientResult.Succeeded || existingClientResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Client not found");

        var existingClient = existingClientResult.Result;

        await _clientRepository.BeginTransactionAsync();

        try
        {
            await _clientRepository.DeleteAsync(existingClient);
            await _clientRepository.SaveAsync();

            await _clientRepository.CommitTransactionAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception e)
        {
            await _clientRepository.RollbackTransactionAsync();
            return ServiceResult<bool>.Failed(500, e.Message);
        }
    }


    public async Task<ServiceResult<Client>> GetClientAsync(int id)
    {
        var result = await _clientRepository.GetAsync(where: x => x.Id == id);
            
        var client  = result.Result?.MapTo<Client>();

        return result.Succeeded
            ? ServiceResult<Client>.Success(client!)
            : ServiceResult<Client>.Failed(result.StatusCode, "Client not found");

    }

    public async Task<ServiceResult<IEnumerable<Client>>> GetAllClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        var clients = result.Result?.Select(c => c.MapTo<Client>());

        return ServiceResult<IEnumerable<Client>>.Success(clients!);
    }
}
