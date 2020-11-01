using Microsoft.EntityFrameworkCore;
using StudentServiceBL.Data;
using StudentServiceBL.Storage;
using System.Threading.Tasks;

namespace StudentServiceStorage
{
    public class PostgresStorage : IStudentServiceStorage
    {
        private PostgresContext _contextDb;

        public PostgresStorage(string connectionString)
        {
            _contextDb = new PostgresContext(connectionString);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var studentDb = await _contextDb.students.Include(s => s.student_groups).FirstOrDefaultAsync(s => s.id == studentId);
            return PostgresDbHelper.ToStudent(studentDb);
        }
    }
}
