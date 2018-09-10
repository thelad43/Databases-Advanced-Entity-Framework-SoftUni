namespace SoftUni.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IDepartmentService
    {
        IEnumerable<DepartmentManagerWithEmployeesModel> DepartmentManagerWithEmployees();
    }
}