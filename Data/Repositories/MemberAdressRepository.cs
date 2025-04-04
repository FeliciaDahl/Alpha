using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class MemberAdressRepository(DataContext context) : BaseRepository<MemberAdressEntity, MemberAdress>(context), IMemberAdressRepository
{

}
