using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FluentAPI.EF
{
    public static class Validator
    {
        private static DateTime domainStart = new DateTime(1950, 01, 01);

        /// <summary>
        /// Shared validation methods.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidName(string text)
        {
            bool valid = false;

            if (string.IsNullOrWhiteSpace(text))
            {

            }
            else if (text.Length > 100)
            {

            }
            else if (!Regex.IsMatch(text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                valid = true;
            }

            return valid;
        }

        public static bool IsValidDescription(string text)
        {
            bool valid = false;

            if (string.IsNullOrWhiteSpace(text))
            {

            }
            else if (text.Length > 1000)
            {

            }
            else
            {
                valid = true;
            }

            return valid;
        }

        public static bool IsValidStartDate(DateTime startDate)
        {
            bool valid = false;

            if(startDate < domainStart)
            {

            }
            else
            {
                valid = true;
            }

            return valid;
        }

        public static bool IsValidEndDate(DateTime endDate, DateTime startDate)
        {
            bool valid = false;

            if(endDate < startDate)
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }

        public static bool IsValidAmount(decimal money)
        {
            bool valid = false;

            if(money < 0)
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }

        /// <summary>
        /// Employee validation methods.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static bool IsValidBirthDate(DateTime birthDate)
        {
            bool valid = false;

            if(birthDate > DateTime.Now)
            {

            }
            else if(birthDate < DateTime.Today.AddYears(-70) )
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }

        public static bool IsValidHiringDate(DateTime hiringDate, DateTime birthDate)
        {
            bool valid = false;

            if(hiringDate > DateTime.Today)
            {

            }
            else if(hiringDate < birthDate)
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }
        public static bool IsValidCPR(int cpr)
        {
            bool valid = false;

            if (cpr.ToString().Length != 10)
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }

        /// <summary>
        /// ContactInfo validation methods.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
    public static bool IsValidEmail(string email)
        {
            bool valid = false;

            if (!email.Contains("@"))
            {

            }
            else if (!email.EndsWith(".dk") || !email.EndsWith(".com"))
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }
    public static bool IsvalidPhone(int phone)
        {
            bool valid = false;

            if (phone.ToString().Length > 6)
            {

            }
            else
            {
                valid = true;
            }
            return valid;
        }
    }
}
