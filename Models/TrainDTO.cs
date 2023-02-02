using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class TrainDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Train name"), MaxLength(100)]
        public string Name { get; set; }
        public int Length { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
    }
}
