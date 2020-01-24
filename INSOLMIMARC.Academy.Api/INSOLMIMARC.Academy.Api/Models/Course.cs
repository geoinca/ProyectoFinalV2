using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Api.Models
{
    public class Course
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public DateTime Timestamp { get; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
