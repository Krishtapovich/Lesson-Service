using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<Question> Questions { get; set; }
    }

    public class SurveyDto
    {
        public Guid Id { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
