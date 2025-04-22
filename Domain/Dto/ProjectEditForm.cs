using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto;

public class ProjectEditForm
{
    public int Id { get; set; }
    public string? ProjectImagePath { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Budget { get; set; }

    public int ClientId { get; set; }

    public string? ProjectMemberId { get; set; } = null!;

    public int StatusId { get; set; }
}
