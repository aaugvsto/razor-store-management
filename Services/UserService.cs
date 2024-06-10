using DataAccess;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(DBContext context) : base(context)
        {
        }

        public async Task<bool> Authenticate(LoginDto dto)
        {
            return await dbSet.AnyAsync(x => x.Email == dto.Email && x.Password == dto.Password);
        }
    }
}
