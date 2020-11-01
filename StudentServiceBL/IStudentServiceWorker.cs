using StudentServiceBL.Data;
using System.Threading.Tasks;

namespace StudentServiceBL
{
    public interface IStudentServiceWorker
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<PaginatedList<Student>> GetFilteredStudentAsync(string filterText, int pageIndex, int pageSize);
    }
}
