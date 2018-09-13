namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        string firstName;
        string lastName;
        DateTime birthDate;
        DateTime hiringDate;
        int cpr;
        decimal pay;
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(FirstName)} feltet m� ikke v�re tomt");
                }
                firstName = value;
            }
        }

        [Required]
        [StringLength(100)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(LastName)} feltet m� ikke v�re tomt");
                }
                lastName = value;
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (DateTime.Today.Year - value.Year > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(BirthDate)} alder kan ikke overskride 100 �r");
                }
                birthDate = value;
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime HiringDate
        {
            get
            {
                return hiringDate;
            }
            set
            {
                if (value < birthDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(HiringDate)} hyringsdato kan ikke v�re �ldre end f�dselsdato");
                }
                hiringDate = value;
            }
        }

        public int CPR 
        {
            get
            {
                return cpr;
            }
            set
            {
                if (value.ToString().Length != 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(CPR)} nummeret skal best� af 8 cifre");
                }
                cpr = value;
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Pay)} bel�bet kan ikke v�re mindre end 0");
                }
                pay = value;
            }
        }

        public int? TeamId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Team Team { get; set; }
    }
}
