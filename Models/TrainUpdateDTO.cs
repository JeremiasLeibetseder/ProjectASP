using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class TrainUpdateDTO
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public int Length { get; set; }
        public bool IsActive { get; set; }
    }
}
