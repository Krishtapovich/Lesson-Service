using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }
        [NotMapped]
        public int? ActivePeriod { get; set; }
        public long GroupNumber { get; set; }
        [NotMapped]
        public IEnumerable<long> StudentIds { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
