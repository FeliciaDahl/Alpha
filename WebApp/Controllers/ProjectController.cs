using Business.Interfaces;
using Business.Services;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController(IProjectService projectService, IFileService fileService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IFileService _fileService = fileService;

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> AddProject(ProjectViewModel model)
    {
        var form = model.ProjectRegistration;

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

        if (model.ProjectRegistration.ProjectImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.ProjectRegistration.ProjectImage, "projects");
            model.ProjectRegistration.ProjectImagePath = filePath;
        }

        var registrationForm = form.MapTo<ProjectRegistrationForm>();

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

        if (project == null)
        {
            return NotFound();
        }

        var model = project.Result?.MapTo<ProjectEditViewModel>();

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


        var editForm = model.MapTo<ProjectEditForm>();

        var result = await _projectService.EditProjectAsync(id, editForm);

        if (result.Succeeded)
        {
            return RedirectToAction("Projects", "Admin");
        }

        return BadRequest(new { sucess = false });

    }

}
