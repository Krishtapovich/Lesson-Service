using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public ICollection<Question> Questions { get; set; }
    }

    public class SurveyDto
    {
        public Guid Id { get; set; }
        public bool isClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }

    public class SurveyToGroups
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public int? OpenPeriod { get; set; }
    }
}
