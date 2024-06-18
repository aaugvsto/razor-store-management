using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Phone { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "O campo Logo é obrigatório.")]
        public string ImageBase64 { get; set; }

        public virtual User? User { get; set; }
        public virtual IList<Table> Tables { get; set; }
    }
}
