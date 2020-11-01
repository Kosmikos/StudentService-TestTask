using StudentServiceBL.Data;
using System.Collections.Generic;

namespace StudentServiceAPI.Models
{
    public class ApiResponseFilteredStudent:ApiResponseBase
    {
        public int PageIndex { get; set; }
        public int CountOnPage { get; set; }
        public int CountAll { get; set; }
        public List<Student> Students { get; set; }
    }
}
