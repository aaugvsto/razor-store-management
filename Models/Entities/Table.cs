using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Table : Entity
    {
        public int? StoreId { get; set; }

        [Display(Name = "Identificador")]
        [Required(ErrorMessage = "O campo Identificador é obrigatório.")]
        public string Identifier { get; set; }

        [Display(Name = "Número de assentos")]
        [Required(ErrorMessage = "O campo Número de assentos é obrigatório.")]
        public int SeatsNumber { get; set; }

        [Display(Name = "Mesa disponível?")]
        [Required(ErrorMessage = "O campo Mesa disponível é obrigatório.")]
        public bool IsAvailable { get; set; }

        public virtual Store? Store { get; set; }

        [NotMapped]
        [Display(Name = "Mesa disponível?")]
        public string IsAvailableDisplay 
        { 
            get 
            {
                return IsAvailable ? "Sim" : "Não";
            } 
        }
    }
}
