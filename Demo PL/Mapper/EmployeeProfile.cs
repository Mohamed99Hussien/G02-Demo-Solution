using AutoMapper;
using Demo_DAL.Entities;
using Demo_PL.ViewModels;

namespace Demo_PL.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
