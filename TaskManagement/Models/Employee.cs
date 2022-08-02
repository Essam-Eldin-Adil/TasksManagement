using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 30)]
        public string Position { get; set; }
    }
}
