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
    public class AdminController(IClientService clientService, IMemberService memberService, IProjectService projectService) : Controller
    {
        private readonly IClientService _clientService = clientService;
        private readonly IMemberService _memberService = memberService;
        private readonly IProjectService _projectService = projectService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Projects()
        {
            var projectResult = await _projectService.GetAllProjectsAsync();
            var viewModel = new ProjectViewModel
            {
                Projects = projectResult.Result!.ToList(),

                ProjectRegistration = new ProjectRegistrationViewModel(),
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Members()
        {
            var membersResult = await _memberService.GetAllMembersAsync();
            var viewModel = new MemberViewModel
            {
                Members = membersResult.Result!.ToList(),

                MemberRegistration = new MemberRegistrationViewModel(),
            };
            return View(viewModel);
        }

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


    }
}
