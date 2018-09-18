namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents an employee, inheritst from Person
    /// </summary>
    public partial class Employee : Person
    {

        DateTime hiringDate;
        decimal pay;

        public Employee(string firstName, string lastName, int cpr, DateTime birthDate, DateTime hiringdate, decimal pay)
            :base(firstName, lastName, cpr, birthDate)
        {
            Pay = pay;
        }
        public Employee()
            :base(null, null, 0, DateTime.Now)
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
        public decimal Pay
        {
            get
            {
                return pay;
            }
            set
            {
                if (!Validator.IsValidAmount(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Pay)} Ugyldigt beløb. Beløbet kan ikke være mindre end nul.");
                }
                pay = value;
            }
        }

        public int? TeamId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Team Team { get; set; }
    }
}
