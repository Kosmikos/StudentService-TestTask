using StudentServiceBL.Data;
using StudentServiceStorage.Data;
using System;

namespace StudentServiceStorage
{
    internal static class PostgresDbHelper
    {
        public static Student ToStudent(StudentDb studentDb)
        {
            if (studentDb == null)
                return null;

            return new Student
            {
                Id = studentDb.id,
                Surname = studentDb.surname,
                Name = studentDb.name,
                Patronymic = studentDb.patronymic,
                UniqCode = studentDb.uniq_code,
                Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), studentDb.gender)
            };
        }
    }
}
