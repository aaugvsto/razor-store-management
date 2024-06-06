using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Display(Name = "Nome completo")]
        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        public string FullName { get; set; }

        [Display(Name = "Ativo?")]
        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        public bool IsActive { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
