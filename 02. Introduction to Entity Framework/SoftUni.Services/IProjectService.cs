namespace SoftUni.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IProjectService
    {
        IEnumerable<ProjectNameDescrStartDateModel> Last10StartedProjects();

        void Delete(int id);

        IEnumerable<string> TakeProjectNames(int count);
    }
}