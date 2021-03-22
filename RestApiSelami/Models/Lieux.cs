using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiSelami.Models
{
    public class Lieux
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can only be 50 caracters long")]
        public string Name { get; set; }

        public string Adresse { get; set; }
    }
}
