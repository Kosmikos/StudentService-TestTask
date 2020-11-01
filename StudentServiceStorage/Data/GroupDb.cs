using System.Collections.Generic;

namespace StudentServiceStorage.Data
{
    public class GroupDb
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<StudentGroupDb> student_groups { get; set; }

    }
}
