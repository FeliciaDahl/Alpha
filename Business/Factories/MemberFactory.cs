
using Business.Models;
using Data.Entites;
using Domain.Dto;
using Domain.Models;

namespace Business.Factories;

public class MemberFactory
{

    public static MemberEntity ToEntity(MemberSignUpForm form)
    {
        return form == null ? new MemberEntity() : new MemberEntity()
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            PhoneNumber = form.PhoneNumber,
            Email = form.Email,
          
        };
    }

    public static Member ToModel(MemberEntity user)
    {
        return user == null ? new Member() : new Member()
        {
           
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber
        };
    }

}
  
