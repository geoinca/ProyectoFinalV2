using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace INSOLMIMARC.Academy.Webb.Models
{
    public class Teacher
    {
        public int ID { get; set; }

        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

    }
}
