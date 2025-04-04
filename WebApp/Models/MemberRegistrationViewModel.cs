﻿using Business.Models;
using Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MemberRegistrationViewModel
{

    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter a first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter a last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a email")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter a email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phone number")]
    public string? Phone { get; set; }

    //public static implicit operator MemberRegistrationForm(MemberRegistrationViewModel model)
    //{
    //    return model == null ? null! : new MemberRegistrationForm
    //    {
    //        FirstName = model.FirstName,
    //        LastName = model.LastName,
    //        Email = model.Email,
    //        Phone = model?.Phone,

    //    };
    //}

}
