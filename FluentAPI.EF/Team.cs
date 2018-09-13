namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        private string name;
        private string description;
        private DateTime startDate;
        private DateTime expectedEndDate;

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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Name)} feltet må ikke være tomt");
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
                if (value < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(StartDate)} feltet må ikke være tomt");
                }
                startDate = value;
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime ExpectedEndDate
        {
            get
            {
                return expectedEndDate;
            }
            set
            {
                if (value < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(ExpectedEndDate)} feltet må ikke være tomt");
                }
                expectedEndDate = value;
            }
        }

        public int? ProjectId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Project Project { get; set; }
    }
}
