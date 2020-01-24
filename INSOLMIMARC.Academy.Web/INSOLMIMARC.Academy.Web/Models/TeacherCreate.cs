using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Web.Models
{
    public class TeacherCreate
    {
       
            public int ID { get; set; }
            public string LastName { get; set; }
            public string FirstMidName { get; set; }
            public string Dni { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }

            public string Job { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public string Phone { get; set; }
       
    }
}
