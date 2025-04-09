
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class ProjectRegistrationForm
{

    public string? Image { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Budget { get; set; }

    public DateTime Created { get; set; }

    public int ClientId { get; set; }

    public string ProjectMemberId { get; set; } = null!;

    public int StatusId { get; set; }

}
