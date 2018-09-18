namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;

    public partial class Project : IWorkable
    {
        private string name;
        private string description;
        private DateTime startDate;
        private DateTime endDate;
        private decimal budget;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Teams = new HashSet<Team>();
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
                        value, $"{nameof(Name)} Ugyldigt navn. Et navn kan kun bestå af bogstaver, højst 100 karakterer og feltet må ikke være blankt.");
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
                        value, $"{nameof(Description)} Ugyldig beskrivelse. Beskrivelse må maks indeholde 1000 karakterer og feltet må ikke være tomt");
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
                        value, $"{nameof(StartDate)} Ugyldig dato. Projektet kan ikke startes før firmaets stiftelsesdato(1950)");
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
                        value, $"{nameof(EndDate)} Ugyldig dato. Slutdato kan ikke være før Startdato.");
                }
                endDate = value;
            }
        }

        [Column(TypeName = "money")]
        public decimal Budget
        {
            get
            {
                return budget;
            }
            set
            {
                if (!Validator.IsValidAmount(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Budget)} Ugyldigt beløb. Beløbet kan ikke være mindre end nul.");
                }
                budget = value;
            }
        }
        public decimal Calculate()
        {
            decimal amount = 0;
            return amount;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
