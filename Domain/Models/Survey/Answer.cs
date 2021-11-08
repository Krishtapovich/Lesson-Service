using System;

namespace Domain.Models.Survey
{
    public class Answer
    {
        public Guid Id { get; set; }
        public long StudentId { get; set; }
        public Guid SurveyId { get; set; }
        public string QuestionId { get; set; }
        public Option Option { get; set; }
    }
}
