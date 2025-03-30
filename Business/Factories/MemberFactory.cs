
using Business.Models;
using Data.Entites;

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
            Email = form.Email,
          
        };
    }

    public static Member ToModel(MemberEntity user)
    {
        return user == null ? new Member() : new Member()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber
        };
    }

}
