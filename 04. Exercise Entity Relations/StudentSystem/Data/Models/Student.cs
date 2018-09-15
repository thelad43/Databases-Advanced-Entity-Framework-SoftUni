namespace StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [RegularExpression("[A-Za-z0-9]{10}")]
        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public List<HomeworkSubmission> Homeworks { get; set; } = new List<HomeworkSubmission>();

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
    }
}