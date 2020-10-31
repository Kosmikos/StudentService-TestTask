using StudentServiceBL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentServiceBL
{
    public interface IStudentServiceWorker
    {
        Task<Student> GetStudentByIdAsync(int studentId);
    }
}
