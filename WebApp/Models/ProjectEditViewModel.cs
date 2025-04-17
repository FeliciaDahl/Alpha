using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ProjectEditViewModel
{
    public int Id { get; set; }

    [Display(Name = "Project Image", Prompt = "Enter image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    public string? ProjectImagePath { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Budget { get; set; }

    public DateTime Created { get; set; }

    public int ClientName { get; set; }

    public int Status { get; set; }

}
