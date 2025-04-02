
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Data.Models;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using System.Diagnostics;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;


    public async Task<ServiceResult<bool>> CreateClientAsync(ClientRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.ClientName))
            return ServiceResult<bool>.Failed(400, "Client name can not be empty");

        var clientExist = await _clientRepository.ExistsAsync(c => c.Email == form.Email);

        if (!clientExist.Succeeded)
            return ServiceResult<bool>.Failed(500, "Something went wrong");

        if (clientExist.Result)
            return ServiceResult<bool>.Failed(400, "Client with this email already exist");

        await _clientRepository.BeginTransactionAsync();

        try
        {
            var clientEntity = form.MapTo<ClientEntity>();
            await _clientRepository.AddAsync(clientEntity);
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

    public async Task<ServiceResult<bool>> EditClientAsync(int id, ClientRegistrationForm form)
    {

        var existingClientResult = await _clientRepository.GetAsync(c => c.Id == id);

        if (!existingClientResult.Succeeded || existingClientResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Client not found");

        var existingClient = existingClientResult.Result;


        existingClient.ClientName = form.ClientName;
        existingClient.ContactPerson = form.ContactPerson;
        existingClient.Email = form.Email;
        existingClient.Location = form.Location;
        existingClient.Phone = form.Phone;

        await _clientRepository.BeginTransactionAsync();

        try
        {

            await _clientRepository.UpdateAsync(existingClient);
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
        var existingClientResult = await _clientRepository.GetAsync(c => c.Id == id);
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



    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }
}
