using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}