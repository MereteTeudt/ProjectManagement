namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents an employee, inheritst from Person. In addition to the properties of a person, an employee also has a hiringdate and salary
    /// </summary>
    public partial class Employee : Person
    {
        DateTime hiringDate;
        decimal salary;

        // Constructor to show constructor chaining
        public Employee(string firstName, string lastName, string cpr, DateTime birthDate, DateTime hiringdate, decimal salary)
            :base(firstName, lastName, cpr, birthDate)
        {
            Salary = salary;
            HiringDate = hiringdate;
        }
        public Employee()
            : base()
        { }
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime HiringDate
        {
            get
            {
                return hiringDate;
            }
            set
            {
                if (!Validator.IsValidHiringDate(value, BirthDate))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(HiringDate)} Ugyldig hyringsdato.Hyringsdato skal være senere end den ansattes fødselsdato og firmaets stiftelse.");
                }
                hiringDate = value;
            }
        }
        [Column(TypeName = "money")]
        public decimal Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if (!Validator.IsValidAmount(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Salary)} Ugyldigt beløb. Beløbet kan ikke være mindre end nul.");
                }
                salary = value;
            }
        }

        public int? TeamId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Team Team { get; set; }
    }
}
