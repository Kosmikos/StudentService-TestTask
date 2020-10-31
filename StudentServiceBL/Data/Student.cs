namespace StudentServiceBL.Data
{
    public class Student
    {
        public int Id { get; set; }
        public GenderEnum Gender { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string UniqCode { get; set; }
    }
}
