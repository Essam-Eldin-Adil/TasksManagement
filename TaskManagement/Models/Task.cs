using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 255)]
        public string Description { get; set; }
        public bool Status { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string Position { get; set; }
    }
}
