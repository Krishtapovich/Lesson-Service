using System.Collections.Generic;

namespace Domain.Models.Student
{
    public class Group
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}