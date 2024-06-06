using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Table : Entity
    {
        public int StoreId { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        public string Number { get; set; }

        [Display(Name = "Número de assentos")]
        [Required(ErrorMessage = "O campo Número de assentos é obrigatório.")]
        public int SeatsNumber { get; set; }

        [Display(Name = "Mesa disponível?")]
        [Required(ErrorMessage = "O campo Mesa disponível é obrigatório.")]
        public bool IsAvailable { get; set; }

        public virtual Store Store { get; set; }
    }
}
