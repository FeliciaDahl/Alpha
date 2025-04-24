using Business.Interfaces;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController(IProjectService projectService, IFileService fileService, IClientService clientService, IStatusService statusService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;
    private readonly IFileService _fileService = fileService;

    public async Task<IActionResult> Index(int? statusId = null)
    {

        var viewModel = new ProjectViewModel
        {
            Projects = await LoadProjectListAsync(),
            ClientList = await LoadClientListAsync(),
            StatusList = await LoadStatusListAsync(),
            ProjectRegistration = new ProjectRegistrationViewModel()
            {
                ClientList = new List<SelectListItem>(),


            },
            ProjectEdit = new ProjectEditViewModel()
            {
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


    [HttpPost]
    public async Task<IActionResult> AddProject(ProjectRegistrationViewModel model)
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

        if (model.ProjectImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.ProjectImage, "projects");
            model.ProjectImagePath = filePath;
        }

        var registrationForm = model.MapTo<ProjectRegistrationForm>();

        var result = await _projectService.CreateProjectAsync(registrationForm);
        if (result.Succeeded)
        {
            return RedirectToAction("Projects", "Admin");
        }

        return BadRequest(new { sucess = false });
    }


    [HttpGet]
    public async Task<IActionResult> EditProject(int id)
    {

        var project = await _projectService.GetProjectAsync(id);

        var result = project.Result;
        if (result == null)
        {
            return NotFound();
        }

        var clients = await _clientService.GetAllClientsAsync();
        var clientList = clients.Result!.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.ClientName
        }).ToList();

        var statuses = await _statusService.GetAllStatusesAsync();
        var statusList = statuses.Result!.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.StatusName
        }).ToList();

        var model = new ProjectEditViewModel
        {
            Id = result.Id,
            ProjectImagePath = result.Image,
            Title = result.Title,
            Description = result.Description,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Budget = result.Budget,
            ClientId = result.Client.Id,
            StatusId = result.Status.Id,
            ClientList = clientList,
            StatusList = statusList

        };

        return Ok(model);
    }


    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> EditProject(int id, [FromForm] ProjectEditViewModel model)
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


        if (model.ProjectImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.ProjectImage, "projects");
            model.ProjectImagePath = filePath;
        }

        var editForm = model.MapTo<ProjectEditForm>();

        var result = await _projectService.EditProjectAsync(id, editForm);

        if (result.Succeeded)
        {
            return RedirectToAction("Projects", "Admin");
        }

        return BadRequest(new { sucess = false });

    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> DeleteProject(int id)
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

        var result = await _projectService.DeleteProjectAsync(id);

        if (result.Succeeded)
        {
            return RedirectToAction("Projects", "Admin");
        }

        return BadRequest(new { sucess = false });

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
