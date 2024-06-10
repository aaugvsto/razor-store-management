using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
   public record CreateTableDto(string Number, int SeatsNumber, bool IsAvailable, int StoreId);
}
