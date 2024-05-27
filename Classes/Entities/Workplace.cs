using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Entities
{
    public class Workplace
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Missing bussiness name!")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Missing City name!")]
        [StringLength(30)]
        public string? City { get; set; }

        [Required(ErrorMessage = "Missing holder name!")]
        [StringLength(50)]
        public string? Holder { get; set; }

        public DateTime DateCreated { get; set; }

        public double RatingWithStars { get; set; }
    }
}
