using StudentServiceBL.Data;
using StudentServiceBL.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentServiceBL
{
    public class StudentServiceWorker: IStudentServiceWorker
    {
        IStudentServiceStorage _storage;

        public StudentServiceWorker(IStudentServiceStorage storage)
        {
            _storage = storage;
        }

        public async Task<PaginatedList<Student>> GetFilteredStudentAsync(string filterText, int pageIndex, int pageSize)
        {
            return await _storage.GetFilteredStudentAsync(filterText, pageIndex, pageSize);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _storage.GetStudentByIdAsync(studentId);
        }
    }
}
