using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid SurveyId { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<QuestionMessage> Messages { get; set; }
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
