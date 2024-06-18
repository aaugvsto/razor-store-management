using DataAccess;
using Models.Entities;
using Services.Base;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        public StoreService(DBContext context) : base(context)
        {
        }
    }
}
