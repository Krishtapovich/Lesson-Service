namespace Domain.Models.Survey
{
    public class QuestionMessage
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string PollId { get; set; }

        public long StudentId { get; set; }
        public Student.Student Student { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
