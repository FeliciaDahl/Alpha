using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApp.Models;

public class ProjectViewModel
 {

    public ProjectViewModel()
    {
        Projects = new List<Project>();
        ClientList = new List<SelectListItem>();
        ProjectRegistration = new ProjectRegistrationViewModel();
        ProjectEdit = new ProjectEditViewModel();
    }


    public List<Project> Projects { get; set; } = new();
    public List<Project> ProjectsStarted => Projects.Where(p => p.StatusId == 1).ToList();
    public List<Project> ProjectsCompleted => Projects.Where(p => p.StatusId == 2).ToList();
    public List<Project> ProjectsOnHold => Projects.Where(p => p.StatusId == 3).ToList();

    public int TotalCount => Projects.Count;
    public int StartedCount => ProjectsStarted.Count;
    public int CompletedCount => ProjectsCompleted.Count;
    public int OnHoldCount => ProjectsOnHold.Count;


    public List<SelectListItem> ClientList { get; set; } = new ();
    public List<SelectListItem> StatusList { get; set; } = new();
    public ProjectRegistrationViewModel ProjectRegistration { get; set; } 
    public ProjectEditViewModel ProjectEdit { get; set; } = new();
}
