using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.EF
{
    // Represents a person with firstname, lastname, cpr and birthdate
    public abstract class Person
    {
        protected string firstName;
        protected string lastName;
        protected string cpr;
        protected DateTime birthDate;

        public Person(string firstName, string lastName, string cpr, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            CPR = cpr;
            BirthDate = birthDate;
        }

        public Person()
        { }
        //Validates that a name can only be letters, cannot be nothing and must be within the bounds of the database
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (!Validator.IsValidName(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(FirstName)} Ugyldigt navn. Et navn kan kun bestå af bogstaver, højst 100 karakterer og feltet må ikke være blankt.");
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
                if (!Validator.IsValidName(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(LastName)} Ugyldigt navn. Et navn kan kun bestå af bogstaver, højst 100 karakterer og feltet må ikke være blankt.");
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
                if (!Validator.IsValidBirthDate(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(BirthDate)} Ugyldig alder. Ansatte kan ikke være over 70 år.");
                }
                birthDate = value;
            }
        }


        public string CPR
        {
            get
            {
                return cpr;
            }
            set
            {
                if (!Validator.IsValidCPR(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(CPR)} Ugyldigt CPR nummer. Et CPR nummer kan kun bestå af tal.");
                }
                cpr = value;
            }
        }
    }
}
