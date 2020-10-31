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

        public void AddStudent(Student student)
        {
            //_storage.AddStudent(student);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _storage.GetStudentByIdAsync(studentId);
        }
    }
}
