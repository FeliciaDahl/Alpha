﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ProjectEditViewModel
{
    public int Id { get; set; }

    [Display(Name = "Project Image", Prompt = "Enter image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    public string? ProjectImagePath { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a title")]
    [DataType(DataType.Text)]
    [Display(Name = "Title", Prompt = "Enter project title")]
    public string Title { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Description", Prompt = "Enter project description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "You must enter a start date")]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date", Prompt = "Enter project start date")]
  
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = "You must enter a end date, you can always go back and edit")]
    [DataType(DataType.Date)]
    [Display(Name = "End Date", Prompt = "Enter project end date")]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Budget", Prompt = "Enter project budget")]
    [Range(0, 10000000, ErrorMessage = "Budget had to be between 0 - 10.000000sek.")]
    public decimal? Budget { get; set; }

    [Required(ErrorMessage = "You must select a client")]
    [Display(Name = "Client", Prompt = "Select a client")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Select a status for project")]
    [Display(Name = "Status", Prompt = "Select a status")]
    public int StatusId { get; set; }

    public List<SelectListItem> ClientList { get; set; } = new();
    public List<SelectListItem> StatusList { get; set; } = new();
}
