using System;
using System.Collections.Generic;

namespace Domain.Models.Survey
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
