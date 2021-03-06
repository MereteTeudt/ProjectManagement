namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a team of employees, implements the IWorkable interface
    /// </summary>
    public partial class Team : IWorkable
    {
        private string name;
        private string description;
        private DateTime startDate;
        private DateTime endDate;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!Validator.IsValidName(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Name)} Ugyldigt navn. Et navn kan kun best� af bogstaver, h�jst 100 karakterer og feltet m� ikke v�re blankt.");
                }
                name = value;
            }
        }

        [Required]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (!Validator.IsValidDescription(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Description)} Ugyldig beskrivelse. Beskrivelse m� maks indeholde 1000 karakterer og feltet m� ikke v�re tomt");
                }
                description = value;
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (!Validator.IsValidStartDate(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(StartDate)} Ugyldig dato. Holdet kan ikke startes f�r firmaets stiftelsesdato(1950)");
                }
                startDate = value;
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (!Validator.IsValidEndDate(value, StartDate))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(endDate)} Ugyldig dato. Slutdato kan ikke v�re f�r Startdato.");
                }
                endDate = value;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return EndDate - StartDate;
            }
        }

        /// <summary>
        /// Calculates and returns the sum of the salary for all employees in the team per month
        /// </summary>
        /// <returns></returns>
        public decimal Calculate()
        {
            decimal amount = 0;
            foreach(Employee e in Employees)
            {
                amount += e.Salary;
            }
            return amount;
        }
        public int? ProjectId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Project Project { get; set; }
    }
}
