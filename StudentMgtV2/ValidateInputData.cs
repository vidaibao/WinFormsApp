using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMgtV2
{
    internal class ValidateInputData
    {
        /// <summary>
        /// ID is read-only and unique field
        /// System generate new ID for Adding student
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool ValidateStringID(string s) // user can not input ID
        {
            return true;
        }
        /// <summary>
        /// Name string length 6 - 20, not null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool ValidateStringName(string s)
        {
            if ( string.IsNullOrEmpty(s) || s.Length > 20 ) return false;
            return true;
        }
        public bool ValidateStringAddress(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length > 50) return false;
            return true;
        }
        public bool ValidateIntYob(string s)
        {
            if (int.TryParse(s, out int yob))
            {
                if (yob >= 1980 && yob <= 2020) return true;
            }
            return false;
        }
        public bool ValidateDoubleGpa(string s)
        {
            if (double.TryParse(s.Trim(), out double gpa))
            {
                if (gpa >= 5.0 && gpa <= 10.0) { return true; }
            }
            return false;
        }

        // ---- Search String ----
        public string ValidateStringSearch(string s, string inputFormat)
        {
            if (string.IsNullOrEmpty(s)) return "UserInputIsNullOrEmpty";
            if (s.Split(',').Length != inputFormat.Split(',').Length) return "Not matching with input format.";
            var input = s.ToLower().Split(',');
            var format = inputFormat.ToLower().Split(",");
            for (int i = 0; i < input.Length; i++)
            {
                if (format[i].Equals("id") || format[i].Equals("name") || format[i].Equals("address"))
                {
                    //
                }
                else if (format[i].Equals("yob"))
                {
                    if (!ValidateIntYob(input[i]))
                    {
                        return "Yob must be positive integer and between 1980 to 2020.";
                    }
                }
                else if (format[i].Equals("gpa"))
                {
                    if (!ValidateDoubleGpa(input[i])) return "Yob must be double number and between 5.0 to 10.0";
                }
            }
            return "yes";
        }
    }
}
