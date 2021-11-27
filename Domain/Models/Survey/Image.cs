using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Survey
{
    [Table("Image")]
    public class ImageModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }

        public int AnswerId { get; set; }
        public AnswerModel Answer { get; set; }
    }
}
