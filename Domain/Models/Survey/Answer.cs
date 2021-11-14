namespace Domain.Models.Survey
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionMessageId { get; set; }
        public QuestionMessage QuestionMessage { get; set; }
        public Option Option { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
