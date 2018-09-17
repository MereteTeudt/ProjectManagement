using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.EF
{
    public abstract class Person
    {
        protected string firstName;
        protected string lastName;
        protected int cpr;
        protected DateTime birthDate;

        public Person(string firstName, string lastName, int cpr, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            CPR = cpr;
            BirthDate = birthDate;
        }

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
                        value, $"{nameof(FirstName)} feltet må ikke være tomt");
                }
                firstName = value;
            }
        }

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
                        value, $"{nameof(LastName)} feltet må ikke være tomt");
                }
                lastName = value;
            }
        }

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
                        value, $"{nameof(BirthDate)} alder kan ikke overskride 100 år");
                }
                birthDate = value;
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
                        value, $"{nameof(CPR)} nummeret skal bestå af 8 cifre");
                }
                cpr = value;
            }
        }
    }
}
