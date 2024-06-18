using DataAccess;
using Models.Dtos;
using Models.Entities;
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

        /// <summary>
        /// Authenticates a user based on the provided login data transfer object (DTO).
        /// </summary>
        /// <param name="dto">The login DTO containing the email and password.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the authentication was successful.</returns>
        public async Task<bool> Authenticate(LoginDto dto)
        {
            return await dbSet.AnyAsync(x => x.Email == dto.Email && x.Password == dto.Password);
        }
    }
}
