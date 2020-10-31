using Microsoft.EntityFrameworkCore;
using StudentServiceBL.Data;
using StudentServiceStorage.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentServiceStorage
{
    public class PostgresContext : DbContext
    {
        string _connectionString;

        public DbSet<StudentDb> students { get; set; }

        public PostgresContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
