namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;

    public partial class Project
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Name)} feltet må ikke være tomt");
                }
                if (Regex.IsMatch(value, @"^[\p{L}]+$"))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Name)} navne må kun indeholde bogstaver");
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Description)} feltet må ikke være tomt");
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
                if (value > DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(StartDate)} projektet kan ikke startes i fremtiden");
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
                if (value < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(EndDate)} feltet må ikke være tomt");
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Budget)} beløbet kan ikke være mindre end 0");
                }
                budget = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
