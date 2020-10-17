﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PTCTS.Services
{
    class Validation
    {
        public static bool nullCheck(string entry)
        {//check if string is null
            if (String.IsNullOrEmpty(entry))
                return true;
            return false;
        }

        public static bool emailCheck(string entry)
        {//check if email is valid
            if (!String.IsNullOrEmpty(entry) && IsValidEmail(entry))
                return true;
            return false;
        }

        public static bool IsValidEmail(string email)
        {//checks for valid email
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static string fieldNullCheck(String validation)
        {//adds The following fields are required if string is null. Used for displaying error alerts.
            string newString = validation;

            if (newString == null)
            {
                newString = "The following fields are required: " + Environment.NewLine;
            }

            return newString;
        }
    }
}
