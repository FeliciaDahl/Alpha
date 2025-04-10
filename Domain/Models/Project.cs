﻿namespace Domain.Models;

public class Project
{
 
    public string? Image { get; set; }

    public string Title { get; set; } = null!;
  
    public string? Description { get; set; }
   
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Budget { get; set; }

    public DateTime Created { get; set; } 

    public Client Client { get; set; } = null!;

    public Status Status { get; set; } = null!;

    public ProjectMember ProjectMember { get; set; } = null!;

}
