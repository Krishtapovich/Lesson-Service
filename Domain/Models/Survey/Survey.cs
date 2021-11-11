using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

    public class SurveyDto
    {
        public Guid Id { get; set; }
        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}
