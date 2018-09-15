namespace StudentSystem
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using System;

    public class StartUp
    {
        private readonly static Random random = new Random();

        public static void Main()
        {
            var db = new StudentSystemDbContext();
            InitialSeed(db);
        }

        private static void InitialSeed(StudentSystemDbContext db)
        {
            SeedStudents(db);
            SeedCourses(db);
            SeedResources(db);
            SeedHomeworks(db);
        }

        private static void SeedHomeworks(StudentSystemDbContext db)
        {
            var homeworkNames = new string[]
              {
                "homework1",
                "homework2",
                "homework3",
                "homework4",
                "homework5",
                "homework6",
                "homework7",
                "homework8",
                "homework9",
                "homework10",
                "homework11",
              };

            for (int i = 0; i < homeworkNames.Length; i++)
            {
                var homework = new HomeworkSubmission
                {
                    StudentId = random.Next(1, 10),
                    Content = "Some Random Content",
                    ContentType = (ContentType)((random.Next(1, 4))),
                    CourseId = random.Next(1, 8)
                };

                db.Add(homework);
            }

            db.SaveChanges();
        }

        private static void SeedResources(StudentSystemDbContext db)
        {
            var resourseNames = new string[]
            {
                "LINQ",
                "Primal Data types",
                "Reference Data types",
                "OOP",
                "Files",
                "Defining classes",
                "SQL Queries"
            };

            for (int i = 0; i < resourseNames.Length; i++)
            {
                var resource = new Resource
                {
                    Name = resourseNames[i],
                    CourseId = random.Next(1, 8)
                };

                db.Add(resource);
            }

            db.SaveChanges();
        }

        private static void SeedCourses(StudentSystemDbContext db)
        {
            var courseNames = new string[]
            {
                "C#",
                "Ruby on Rails",
                "JavaScript",
                "Angular",
                "React",
                "OOP Advanced",
                "C# Web Development"
            };

            for (int i = 0; i < courseNames.Length; i++)
            {
                var year = random.Next(1800, 2019);
                var month = random.Next(1, 13);
                var date = random.Next(1, 29);

                var endDate = new DateTime(year, month, date);

                var course = new Course
                {
                    Name = courseNames[i],
                    StartDate = DateTime.Now,
                    EndDate = endDate,
                    Price = random.Next(20, 472),
                };

                db.Add(course);
            }

            db.SaveChanges();
        }

        private static void SeedStudents(StudentSystemDbContext db)
        {
            var names = new string[]
            {
                "Pesho",
                "Gosho",
                "Test",
                "Stamat",
                "Georgi",
                "Ivan",
                "Misho",
                "Nasko",
                "Emko"
            };

            for (int i = 0; i < names.Length; i++)
            {
                var year = random.Next(1800, 2019);
                var month = random.Next(1, 13);
                var date = random.Next(1, 29);

                var birthDate = new DateTime(year, month, date);

                var student = new Student
                {
                    Name = names[i],
                    BirthDate = birthDate,
                };

                db.Add(student);
            }

            db.SaveChanges();
        }
    }
}