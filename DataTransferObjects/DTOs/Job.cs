using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.DTOs
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "Title field cannot be empty!")]
        [StringLength(50)]
        public string? Title { get; set; }


        [Display(Name = "Minimum Salary: ")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "The salary cannot be zero or a negative number!")]
        [Required(ErrorMessage = "Minimum Salary field cannot be empty!")]
        public float MinSalary { get; set; }


        [Display(Name = "Minimum Hours: ")]
        [Required(ErrorMessage = "This field cannot be empty!")]
        public int MinHours { get; set; }


        [Display(Name = "Can job be remote")]
        public bool IsRemoteAvailable { get; set; }


        [Display(Name = "Description: ")]
        [StringLength(100)]
        public string? Description { get; set; }


        [Display(Name = "Creation Date: ")]
        public DateTime JobCreationDate { get; set; }

    }
}
