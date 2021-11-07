using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }
        public long GroupNumber { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
