using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing a job title")]
        [StringLength(50)]
        public string Title { get; set; }

        public double MinSalary { get; set; }

        public int MinHours { get; set; }

        public bool IsRemoteAvailable { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        public DateTime JobCreationDate { get; set; }
    }
}
