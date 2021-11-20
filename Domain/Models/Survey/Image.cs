namespace Domain.Models.Survey
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
