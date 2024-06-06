using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Table : Entity
    {
        public int StoreId { get; set; }
        public string Number { get; set; }
        public int SeatsNumber { get; set; }
        public bool IsAvailable { get; set; }

        public virtual Store Store { get; set; }
    }
}
