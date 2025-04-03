using Business.Interfaces;
using Business.Models;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Authorize]
    public class AdminController(IClientService clientService) : Controller
    {
        private readonly IClientService _clientService = clientService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        //[Authorize(Roles ="admin")]
        public IActionResult Members()
        {
            return View();
        }
        public IActionResult Clients()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var registrationForm = model.MapTo<ClientRegistrationForm>();

            var result = await _clientService.CreateClientAsync(registrationForm);
          if(result.Succeeded)
            {
                return RedirectToAction("Projects");
            }

          ViewBag.ErrorMessage = result.Error;
            return View(model);
        }

    }
}
