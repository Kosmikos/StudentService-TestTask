using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentServiceAPI.Models;
using StudentServiceBL;
using StudentServiceBL.Data;

namespace StudentServiceAPI.Controllers
{
    /// <summary>
    /// Function for works with students
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentServiceWorker _worker;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public StudentsController(IStudentServiceWorker worker)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _worker = worker;
        }

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="studentId">Student identificator</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseStudent), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResponseStudent> GetById(int studentId)
        {
            var res = new ApiResponseStudent();
            var student = await _worker.GetStudentByIdAsync(studentId);
            res.Student = student;
            if (student == null)
                res.SetNotFoundResponse(Response, $"Student with id: {studentId} not found");
            else
                res.SetSuccessResponse(Response);
            return res;
        }

        [HttpGet("FilterStudents")]
        public async Task<ApiResponseFilteredStudent> FilterStudents(string filterText, int pageIndex, int pageSize)
        {
            var res = new ApiResponseFilteredStudent();
            var studentPage = await _worker.GetFilteredStudentAsync(filterText, pageIndex, pageSize);
            res.Students = studentPage;
            res.CountAll = studentPage.TotalCount;
            res.CountOnPage = pageSize;
            res.PageIndex = pageIndex;

            res.SetSuccessResponse(Response);
            return res;
        }


    }
}
