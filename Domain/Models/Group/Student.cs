using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Group
{
    [Table("Student")]
    public class StudentModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupNumber { get; set; }
    }
}