using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Code is required !!")]
        public string Code { get; set; }
        [Required (ErrorMessage ="Name is required !!")]
        [MaxLength(50, ErrorMessage ="MaxLenth Name is 50 chars")]

        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        // Navigation Property [Many]
       public ICollection<Employee> Employees { get; set; }
    }
}
