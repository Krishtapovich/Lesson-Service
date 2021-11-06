using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}