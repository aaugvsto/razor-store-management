using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Entity
    {
        public User() 
        { 
            Stores = new HashSet<Store>(); 
        } 

        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
