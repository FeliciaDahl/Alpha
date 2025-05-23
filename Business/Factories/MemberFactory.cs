﻿
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
            PhoneNumber = form.PhoneNumber,
            MemberAdress = new MemberAdressEntity
            {
                Street = form.Street,
                City = form.City,
                PostalCode = form.PostalCode,
                Country = form.Country
                
            }

        };
    }

    public static Member ToModel(MemberEntity user)
    {
        return user == null ? new Member() : new Member()
        {
            Id = user.Id,
            Image = user.Image,
            FirstName = user.FirstName,
            LastName = user.LastName,
            JobTitle = user.JobTitle,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Street = user.MemberAdress?.Street,
            City = user.MemberAdress?.City,
            PostalCode = user.MemberAdress?.PostalCode,
            Country = user.MemberAdress?.Country
        };
    }

}
  
