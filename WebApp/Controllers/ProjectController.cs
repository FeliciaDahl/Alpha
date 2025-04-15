using Business.Interfaces;
using Business.Services;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProjectController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

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


        var registrationForm = form.MapTo<ProjectRegistrationForm>();

        var result = await _projectService.CreateProjectAsync(registrationForm);
        if (result.Succeeded)
        {
            return RedirectToAction("Projects", "Admin");
        }

        return BadRequest(new { sucess = false });
    }
}
