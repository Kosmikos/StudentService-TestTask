namespace StudentServiceStorage.Data
{
    public class StudentGroupDb
    {
        public int student_id { get; set; }
        public StudentDb students { get; set; }
        public int group_id { get; set; }
        public GroupDb groups { get; set; }
    }
}
