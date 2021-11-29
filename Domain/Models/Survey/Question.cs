using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    [Table("Question")]
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid SurveyId { get; set; }
        public ICollection<OptionModel> Options { get; set; }
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<OptionDto> Options { get; set; }
    }
}
