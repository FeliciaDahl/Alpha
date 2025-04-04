using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class Project
{
 
    public string? Image { get; set; }

    public string Title { get; set; } = null!;
  
    public string? Description { get; set; }
   
    public DateTime StartDate { get; set; }


    public DateTime EndDate { get; set; }


    public decimal? Budget { get; set; }

    public DateTime Created { get; set; } 

    public int ClientId { get; set; }

    public int StatusId { get; set; }
   
}
