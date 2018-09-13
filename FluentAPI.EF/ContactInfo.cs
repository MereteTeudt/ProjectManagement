namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInfos")]
    public partial class ContactInfo
    {
        private string email;
        private string phone;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Email)} feltet må ikke være tomt");
                }
                email = value;
            }
        }

        [StringLength(25)]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Phone)} feltet må ikke være tomt");
                }
                phone = value;
            }
        }

        public virtual Employee Employee { get; set; }
    }
}
