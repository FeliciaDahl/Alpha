
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
            Image = form.MemberImagePath,
            FirstName = form.FirstName,
            LastName = form.LastName,
            JobTitle = form.JobTitle,
            Email = form.Email,
            PhoneNumber = form.PhoneNumber
            
          
        };
    }

    public static Member ToModel(MemberEntity user)
    {
        return user == null ? new Member() : new Member()
        {
            Image = user.Image,
            FirstName = user.FirstName,
            LastName = user.LastName,
            JobTitle = user.JobTitle,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber
        };
    }

}
  
