using Business.Interfaces;
using Data.Entites;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers;

public class ClientController(IClientService clientService, IFileService fileService) : Controller
{
    private readonly IClientService _clientService = clientService;

    private readonly IFileService _fileService = fileService;

    public async Task<IActionResult> Clients()
    {
        var clientResult = await _clientService.GetAllClientsAsync();

        var viewModel = new ClientViewModel
        {
            Clients = clientResult.Result!.ToList(),

            ClientRegistration = new ClientRegistrationViewModel(),

            ClientEdit = new ClientEditViewModel()
        };

        return View(viewModel);

    }

    [HttpPost]
    public async Task<IActionResult> AddClient(ClientRegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()  
                );
            return BadRequest(new {sucess =false, errors });
        }

        if (model.ClientImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.ClientImage, "clients");
            model.ClientImagePath = filePath;
        }

        var registrationForm = model.MapTo<ClientRegistrationForm>();

        var result = await _clientService.CreateClientAsync(registrationForm);
        if (result.Succeeded)
        {
            return RedirectToAction("Clients", "Admin");
        }

        return BadRequest(new { sucess = false });
    }

    [HttpGet]
    public async Task<IActionResult> EditClient(int id)
    {
        var client = await _clientService.GetClientAsync(id);

        var result = client.Result;
        if (result == null)
        {
            return NotFound();
        }

        var model = new ClientEditViewModel
        {
            Id = result.Id,
            ImagePath = result.Image,
            ClientName = result.ClientName,
            ContactPerson = result.ContactPerson,
            Email = result.Email,
            Location = result.Location,
            Phone = result.Phone
      
        };

        return Ok(model);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> EditClient(int id, [FromForm] ClientEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { sucess = false, errors });
        }

        if (model.ClientImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.ClientImage, "clients");
            model.ImagePath = filePath;
        }

        var editForm = model.MapTo<ClientEditForm>();

        var result = await _clientService.EditClientAsync(id, editForm);

        if (result.Succeeded)
        {
            return RedirectToAction("Clients", "Admin");
        }

        return BadRequest(new { sucess = false });

    }



    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { sucess = false, errors });
          
        }

        var result = await _clientService.DeleteClientAsync(id);

        if (result.Succeeded)
        {
            return RedirectToAction("Clients", "Admin");
        }

        return BadRequest(new { sucess = false });

    }

}
