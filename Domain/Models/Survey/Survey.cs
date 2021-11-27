using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    [Table("Survey")]
    public class SurveyModel
    {
        public Guid Id { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public ICollection<QuestionModel> Questions { get; set; }
    }

    public class SurveyDto
    {
        public Guid Id { get; set; }
        public bool IsClosed { get; set; } = true;
        public DateTime CreationTime { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }

    public class SurveyListModel
    {
        public Guid Id { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class SurveySendingModel
    {
        public Guid Id { get; set; }
        public int? OpenPeriod { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
