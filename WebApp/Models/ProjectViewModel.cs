using Domain.Models;

namespace WebApp.Models;

public class ProjectViewModel
{
    public List<Project> Projects { get; set; } = new();
    public ProjectRegistrationViewModel ProjectRegistration { get; set; } = new();
}
