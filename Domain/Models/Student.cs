using System;

namespace Domain.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}