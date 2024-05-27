using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.DTOs.JoinDTOs
{
    public class WorkJobs
    {
        [Key]
        public int Id { get; set; }
        public int JobId { get; set; }
        public int WorkId { get; set; }
        [ForeignKey("JobId")]
        public Job? Job { get; set; }
        [ForeignKey("WorkId")]
        public Workplace? Workplace { get; set; }
    }
}
