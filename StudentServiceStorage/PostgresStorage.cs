using Microsoft.EntityFrameworkCore;
using StudentServiceBL;
using StudentServiceBL.Data;
using StudentServiceBL.Storage;
using StudentServiceStorage.Data;
using System;
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

        public async Task<PaginatedList<Student>> GetFilteredStudentAsync(string filterText, int pageIndex, int pageSize)
        {
            IQueryable<StudentDb> studentsDbIQ = _contextDb.students;
            studentsDbIQ = studentsDbIQ.Include(s => s.student_groups).ThenInclude(g => g.groups);

            if (!String.IsNullOrEmpty(filterText))
            {
                studentsDbIQ = studentsDbIQ.Where(s => s.name.Contains(filterText)
                                       || s.surname.Contains(filterText)
                                       || s.patronymic.Contains(filterText)
                                       || s.uniq_code.Contains(filterText)
                                       || s.gender.Contains(filterText)
                                       || (s.student_groups!=null && s.student_groups.Any(g => g.groups.name.Contains(filterText)))
                                       );
            }


            var students = studentsDbIQ.Select(sdb => PostgresDbHelper.ToStudent(sdb));
            return await PaginatedList<Student>.CreateAsync(students, pageIndex, pageSize);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var studentDb = await _contextDb.students.Include(s => s.student_groups).ThenInclude(g => g.groups).FirstOrDefaultAsync(s => s.id == studentId);
            //var groups = _contextDb.groups.ToList();
            return PostgresDbHelper.ToStudent(studentDb);
        }
    }
}
