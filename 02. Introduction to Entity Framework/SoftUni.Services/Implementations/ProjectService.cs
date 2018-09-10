namespace SoftUni.Services.Implementations
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectService : IProjectService
    {
        private readonly SoftUniDbContext db;

        public ProjectService(SoftUniDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ProjectNameDescrStartDateModel> Last10StartedProjects()
        {
            var projects = this.db
                .Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new ProjectNameDescrStartDateModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate
                })
                .OrderBy(p => p.Name)
                .ToList();

            return projects;
        }

        public void Delete(int id)
        {
            var project = this.db.Projects.Find(id);

            var employeesProjects = this.db.EmployeesProjects.Where(ep => ep.ProjectId == id).ToList();

            foreach (var ep in employeesProjects)
            {
                project.EmployeesProjects.Remove(ep);
            }

            this.db.Remove(project);

            this.db.SaveChanges();
        }

        public IEnumerable<string> TakeProjectNames(int count)
        {
            var projectNames = this.db
                .Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            return projectNames;
        }
    }
}