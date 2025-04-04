
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class MemberAdress
{
    public string UserId { get; set; } = null!;

   
    public string Street { get; set; } = null!;

 
    public string City { get; set; } = null!;

  
    public string PostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;

}
