using StudentServiceBL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentServiceBL.Storage
{
    public interface IStudentServiceStorage
    {
        Task<Student> GetStudentByIdAsync(int studentId);
    }
}
