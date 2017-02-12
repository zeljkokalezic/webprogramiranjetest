using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Workplace
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Employees { get; set; }
    }
}