using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Question
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
        public ICollection<QuestionMessage> Messages { get; set; } = new List<QuestionMessage>();
    }
}
