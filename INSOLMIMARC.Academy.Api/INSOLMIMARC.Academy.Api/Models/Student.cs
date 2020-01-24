using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Api.Models
{
    public class Student
    {
        public int ID { get; set; }
        public int TutorID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime Timestamp { get; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
