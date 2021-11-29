using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Group;

namespace Domain.Models.Survey
{
    [Table("QuestionMessage")]
    public class QuestionMessage
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string PollId { get; set; }

        public long? StudentId { get; set; }
        public StudentModel Student { get; set; }

        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }
}
