using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [CreditCard]
        public int YearOfBirth { get; set; }
        public int DateOfBirth { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Workplace> Workplaces { get; set; }
    }
}