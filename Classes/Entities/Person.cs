using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing a first name")]
        [StringLength(30)]
        public string? FName { get; set; }

        [Required(ErrorMessage = "Missing a last name")]
        [StringLength(30)]
        public string? LName { get; set; }

        [StringLength(16)]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Missing an age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Missing a salary")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Missing a date of birth")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Missing an email")]
        [StringLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        public int JobId { get; set; }

        public int WorkId { get; set; }

    }
}
