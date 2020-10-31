using StudentServiceBL.Data;

namespace StudentServiceAPI.Models
{
    /// <summary>
    /// Result of query one student
    /// </summary>
    public class ApiResponseStudent : ApiResponseBase
    {
        /// <summary>
        /// Student data
        /// </summary>
        public Student Student { get; set; }        
    }
}
