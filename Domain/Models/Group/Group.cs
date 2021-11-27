using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Group
{
    [Table("Group")]
    public class GroupModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}