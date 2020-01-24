using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Api.Models
{
    public class Teacher
    {
        public int ID { get; set; }

        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Dni { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AdemicDegrees { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public DateTime Timestamp { get; }
        public DateTime EnrollmentDate { get; set; }
    }
}
