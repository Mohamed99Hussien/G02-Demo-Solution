using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        
        public string Name { get; set; }
       
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        
        public decimal Salary { get; set; }

        [Display(Name="Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        //[DataType(DataType.EmailAddress)]

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Phone]
        //[DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]

        public DateTime HireDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;


        // [ForeignKey("Department")]
        [Display(Name = "Department")]

        public int ? DepartmentId { get; set; }
        //Navigation Property [One]
        public Department Department { get; set; }

        public string ImageName { get; set; }

    }
}
