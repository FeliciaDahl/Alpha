using Business.Interfaces;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
       
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
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
                

            var registrationForm = model.MapTo<ClientRegistrationForm>();

            var result = await _clientService.CreateClientAsync(registrationForm);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Client updated successfully" });
            }

            return BadRequest(new { sucess = false });
        }




        [HttpPut]
        public async Task<IActionResult> EditClient(int id, ClientEditViewModel model)
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

            var editForm = model.MapTo<ClientEditForm>();

            var result = await _clientService.EditClientAsync(id, editForm);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Client updated successfully" });
            }

            return BadRequest(new { sucess = false });

        }

    }

}

