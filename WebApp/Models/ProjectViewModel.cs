using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApp.Models;

public class ProjectViewModel
 {
    private readonly IClientService _clientService;
    private readonly IProjectService _projectService;

    public ProjectViewModel(IClientService clientService, IProjectService projectService)
    {
        _clientService = clientService;
        _projectService = projectService;
        ProjectRegistration = new ProjectRegistrationViewModel();

    }

    public ProjectViewModel()
    {
    }

    public List<Project> Projects { get; set; } = new();
    public ProjectRegistrationViewModel ProjectRegistration { get; set; } 
    public List<SelectListItem> ClientList { get; set; } = new ();

    public async Task LoadProjectListAsync()
    {
        var projectResult = await _projectService.GetAllProjectsAsync();

        Projects = projectResult.Result!.ToList();
    }

    public async Task LoadClientListAsync()
    {
        var clientResult = await _clientService.GetAllClientsAsync();

        ClientList = clientResult.Result!.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.ClientName
        })
        .ToList();

    }
}
