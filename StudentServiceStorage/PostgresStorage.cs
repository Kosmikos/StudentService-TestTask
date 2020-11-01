using Microsoft.EntityFrameworkCore;
using StudentServiceBL.Data;
using StudentServiceBL.Storage;
using System.Linq;
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
            var studentDb = await _contextDb.students.Include(s => s.student_groups).ThenInclude(g=>g.groups).FirstOrDefaultAsync(s => s.id == studentId);
            //var groups = _contextDb.groups.ToList();
            return PostgresDbHelper.ToStudent(studentDb);
        }
    }
}
