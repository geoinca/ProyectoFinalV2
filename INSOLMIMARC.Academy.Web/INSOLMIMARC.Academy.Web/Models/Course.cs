using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Web.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
    }
}
