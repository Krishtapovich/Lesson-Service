using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories.BotRepository
{
    public interface IBotRepository
    {
        Task AddStudentAsync(Student student);
        Task<bool> CheckIfAuthorizedAsync(long studentId);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
    }
}