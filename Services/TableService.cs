using DataAccess;
using Domain.Entities;
using Services.Base;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TableService : BaseService<Table>, ITableService
    {
        public TableService(DBContext context) : base(context)
        {
        }
    }
}
