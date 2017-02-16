using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Student : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        public bool Graduated { get; set; }
        public YearOfStudy YearOfStudy { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Graduated && YearOfStudy != YearOfStudy.Final)
            {
                yield return new ValidationResult("Student must be final year student before graduation", new[] { nameof(Graduated) });
            }
        }
    }

    public enum YearOfStudy
    {
        I,
        II,
        III,
        IV,
        Final
    }
}