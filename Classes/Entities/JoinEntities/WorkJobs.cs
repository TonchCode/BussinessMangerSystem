using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Entities.JoinEntities
{
    public class WorkJobs
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Missing JobId")]
        public int JobId { get; set; }


        [Required(ErrorMessage = "Missing WorkId")]
        public int WorkId { get; set; }
    }
}
