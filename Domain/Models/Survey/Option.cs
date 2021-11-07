using System;

namespace Domain.Models.Survey
{
    public class Option
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
