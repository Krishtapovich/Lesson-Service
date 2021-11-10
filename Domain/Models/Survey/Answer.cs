using System;

namespace Domain.Models.Survey
{
    public class Answer
    {
        public int Id { get; set; }
        public long StudentId { get; set; }
        public Guid SurveyId { get; set; }
        public long QuestionId { get; set; }
        public Option Option { get; set; }
        public string Text { get; set; }
    }
}
