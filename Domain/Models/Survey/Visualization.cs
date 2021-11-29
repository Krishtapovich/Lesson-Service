using System.Collections.Generic;

namespace Domain.Models.Results
{
    public class OptionVisualizationModel
    {
        public string OptionText { get; set; }
        public int AnswersAmount { get; set; }
    }

    public class QuestionVisualizationModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<OptionVisualizationModel> Options { get; set; }
    }
}
