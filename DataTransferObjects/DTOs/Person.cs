using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.DTOs
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "Missing a first name")]
        [StringLength(30)]
        public string? FName { get; set; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "Missing a last name")]
        [StringLength(30)]
        public string? LName { get; set; }

        [Display(Name = "Gender: ")]
        [StringLength(16)]
        public string? Sex { get; set; }

        [Display(Name = "Age: ")]
        [Required(ErrorMessage = "Missing an age")]
        public int Age { get; set; }

        [Display(Name = "Salary: ")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "The salary cannot be zero or a negative number!")]
        [Required(ErrorMessage = "Missing a salary")]
        public double Salary { get; set; }

        [Display(Name = "Date of Birth: ")]
        [Required(ErrorMessage = "Missing a date of birth")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Missing an email")]
        [StringLength(50)]
        [EmailAddress]
        public string? Email { get; set; }




        [Display(Name = "Job: ")]
        public int? JobId { get; set; }
        [ForeignKey("JobId")]
        public Job? Job { get; set; }

        [Display(Name = "Workplace: ")]
        public int? WorkId { get; set; }
        [ForeignKey("WorkId")]
        public Workplace? Work { get; set; }
    }
}
