using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Web.Models;

namespace INSOLMIMARC.Academy.Web.Models
{
    public class INSOLMIMARCAcademyWebContext : DbContext
    {
        public INSOLMIMARCAcademyWebContext (DbContextOptions<INSOLMIMARCAcademyWebContext> options)
            : base(options)
        {
        }

        public DbSet<INSOLMIMARC.Academy.Web.Models.Course> Course { get; set; }

        public DbSet<INSOLMIMARC.Academy.Web.Models.Teacher> Teacher { get; set; }

        public DbSet<INSOLMIMARC.Academy.Web.Models.Tutor> Tutor { get; set; }
    }
}
