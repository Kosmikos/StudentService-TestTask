namespace StudentServiceStorage.Data
{
    public class StudentGroupDb
    {
        public int student_id { get; set; }
        public StudentDb student { get; set; }
        public int group_id { get; set; }
        public GroupDb groupdb { get; set; }
    }
}
