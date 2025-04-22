namespace Domain.Models;

public class Project
{

    public int Id { get; set; }
    public string? Image { get; set; }

    public string Title { get; set; } = null!;
  
    public string? Description { get; set; }
   
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Budget { get; set; }
    public int StatusId { get; set; }

    public Client Client { get; set; } = null!;

    public Status Status { get; set; } = null!;

    public List<Member> ProjectMembers { get; set; } = new();

}
