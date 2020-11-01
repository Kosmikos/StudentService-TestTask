using StudentServiceBL.Data;
using System.Threading.Tasks;

namespace StudentServiceBL.Storage
{
    public interface IStudentServiceStorage
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<PaginatedList<Student>> GetFilteredStudentAsync(string filterText, int pageIndex, int pageSize);
    }
}
