using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Group;

namespace Domain.Models.Survey
{
    public class CsvModel
    {
        public string GroupNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
