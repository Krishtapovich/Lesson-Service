using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories.BotRepository
{
    public interface IBotRepository
    {
        Task AddStudent(Student student);
        Task<bool> CheckIfAuthorized(long studentId);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Student student);
    }
}