using Business.Interfaces;
using Business.Models;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

using WebApp.Models;

namespace WebApp.Controllers;

//[Authorize]
public class AdminController(IClientService clientService, IMemberService memberService, IProjectService projectService, IStatusService statusService) : Controller
{
    private readonly IClientService _clientService = clientService;
    private readonly IMemberService _memberService = memberService;
    private readonly IProjectService _projectService = projectService;
    private readonly IStatusService _statusService = statusService;


    public IActionResult Index()
    {
        return View();
    }

    //Lägg till route och en index..
    public async Task<IActionResult> Projects(int? statusId = null)
    {
       
        var viewModel = new ProjectViewModel
        {
            Projects = await LoadProjectListAsync(),
            ClientList = await LoadClientListAsync(),
            StatusList = await LoadStatusListAsync(),
            ProjectRegistration = new ProjectRegistrationViewModel() { 
                ClientList = new List<SelectListItem>(),
                

            },
            ProjectEdit = new ProjectEditViewModel() { 
                ClientList = new List<SelectListItem>(), 
                StatusList = new List<SelectListItem>() 
            }
        };

        ViewBag.Filter = statusId;

        viewModel.ProjectRegistration.ClientList = viewModel.ClientList;
        viewModel.ProjectEdit.ClientList = viewModel.ClientList;
        viewModel.ProjectEdit.StatusList = viewModel.StatusList;

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

    private async Task<List<Project>> LoadProjectListAsync()
    {
        var projectResult = await _projectService.GetAllProjectsAsync();
        return projectResult.Result?.ToList() ?? new();
    }

    private async Task<List<SelectListItem>> LoadClientListAsync()
    {
        var clientResult = await _clientService.GetAllClientsAsync();
        return clientResult.Result!.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.ClientName
        }).ToList();
    }

    private async Task<List<SelectListItem>> LoadStatusListAsync()
    {
        var statusResult = await _statusService.GetAllStatusesAsync();
        return statusResult.Result!.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.StatusName
        }).ToList();
    }


}

    
