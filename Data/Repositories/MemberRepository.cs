using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class MemberRepository(DataContext context) : BaseRepository<MemberEntity, Member>(context), IMemberRepository
{


}


