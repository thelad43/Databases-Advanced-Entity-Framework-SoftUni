namespace Employees.App.Infrastructure.Mapping
{
    using AutoMapper;
    using Data.Models;
    using Services.Models;

    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, Employee>();

                cfg.CreateMap<Employee, EmployeeManagerModel>()
                    .ForMember(e => e.Manager, options => options.MapFrom(src => src.Manager.FirstName));
            });
        }
    }
}