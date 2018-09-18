namespace FluentAPI.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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
                if (!Validator.IsValidEmail(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Email)} feltet må ikke være tomt, skal indehold @ og ende på .com eller .dk");
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
                if (!Validator.IsvalidPhone(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Phone)} telefon numre kan kun bestå af tal og må ikke være længere end 25 tegn");
                }
                phone = value;
            }
        }

        public virtual Employee Employee { get; set; }
    }
}
