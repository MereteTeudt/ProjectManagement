namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpectedEndDate { get; set; }

        public int? ProjectId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Project Project { get; set; }
    }
}
