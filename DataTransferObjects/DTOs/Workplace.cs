using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.DTOs
{
    public class Workplace
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "Missing bussiness name!")]
        [StringLength(50)]
        public string? Name { get; set; }


        [Display(Name = "City: ")]
        [Required(ErrorMessage = "Missing City name!")]
        [StringLength(30)]
        public string? City { get; set; }


        [Display(Name = "Holder: ")]
        [Required(ErrorMessage = "Missing holder name!")]
        [StringLength(50)]
        public string? Holder { get; set; }


        [Display(Name = "DateCreated: ")]
        public DateTime DateCreated { get; set; }


        [Display(Name = "Star Rating: ")]
        [Range(0.0, 5.0, ErrorMessage = "The rating must be between 0.0 and 5.0!")]
        public double RatingWithStars { get; set; }
    }
}
