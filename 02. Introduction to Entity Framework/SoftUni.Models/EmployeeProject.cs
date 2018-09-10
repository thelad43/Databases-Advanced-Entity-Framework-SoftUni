namespace SoftUni.Models
{
    public class EmployeesProjects
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}