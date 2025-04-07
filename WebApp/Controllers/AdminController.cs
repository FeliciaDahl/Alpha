using Business.Interfaces;
using Business.Models;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
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
        public async Task<IActionResult> Clients()
        {
            var clientResult = await _clientService.GetAllClientsAsync();

            var viewModel = new ClientViewModel
            {
                Clients = clientResult.Result.ToList(),

                ClientRegistration = new ClientRegistrationViewModel(),
                ClientEdit = new ClientEditViewModel()
            };

            return View(viewModel);

        }


    }
}
