using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Store : Entity
    {
        public Store()
        {
            Tables = new List<Table>();
        }   

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsOpen { get; set; }
        public string ImageBase64 { get; set; }

        public virtual User User { get; set; }
        public virtual IList<Table> Tables { get; set; }
    }
}
