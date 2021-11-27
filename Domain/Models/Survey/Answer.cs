using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    [Table("Answer")]
    public class AnswerModel
    {
        public int Id { get; set; }
        public Guid SurveyId { get; set; }
        public string Text { get; set; }
        public ImageModel Image { get; set; }

        public int? OptionId { get; set; }
        public OptionModel Option { get; set; }

        public int QuestionMessageId { get; set; }
        public QuestionMessage QuestionMessage { get; set; }
    }

    public class AnswerDto
    {
        public int Id { get; set; }
        public OptionModel Option { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
