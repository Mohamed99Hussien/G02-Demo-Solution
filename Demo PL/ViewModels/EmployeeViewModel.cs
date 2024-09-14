using Demo_DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo_PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-Citty-County")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000, 8000)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
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


        // [ForeignKey("Department")]
        [Display(Name = "Department")]

        public int? DepartmentId { get; set; }
        //Navigation Property [One]
        public Department Department { get; set; }

        public IFormFile Image {  get; set; }
        public string ImageName { get; set; }


    }
}
