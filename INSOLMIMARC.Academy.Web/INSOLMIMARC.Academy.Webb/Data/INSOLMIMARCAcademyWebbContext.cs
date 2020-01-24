using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Webb.Models;

namespace INSOLMIMARC.Academy.Webb.Models
{
    public class INSOLMIMARCAcademyWebbContext : DbContext
    {
        public INSOLMIMARCAcademyWebbContext (DbContextOptions<INSOLMIMARCAcademyWebbContext> options)
            : base(options)
        {
        }

        public DbSet<INSOLMIMARC.Academy.Webb.Models.Tutor> Tutor { get; set; }

        public DbSet<INSOLMIMARC.Academy.Webb.Models.Teacher> Teacher { get; set; }

        public DbSet<INSOLMIMARC.Academy.Webb.Models.Course> Course { get; set; }

        public DbSet<INSOLMIMARC.Academy.Webb.Models.TeacherCreate> TeacherCreate { get; set; }
    }
}
