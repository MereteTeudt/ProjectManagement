namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime HiringDate { get; set; }

        public int CPR { get; set; }

        [Column(TypeName = "money")]
        public decimal Pay { get; set; }

        public int? TeamId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Team Team { get; set; }
    }
}
