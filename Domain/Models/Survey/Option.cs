using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    [Table("Option")]
    public class OptionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
