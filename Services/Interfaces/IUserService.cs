using Models.Dtos;
using Models.Entities;
using Services.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> Authenticate(LoginDto dto);
    }
}
