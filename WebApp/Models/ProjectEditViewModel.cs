namespace WebApp.Models;

public class ProjectEditViewModel
{
    public int Id { get; set; }
    public string? Image { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Budget { get; set; }

    public DateTime Created { get; set; }

    public int ClientName { get; set; }

    public int Status { get; set; }

}
