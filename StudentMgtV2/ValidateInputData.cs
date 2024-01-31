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
            if ( string.IsNullOrEmpty(s) || s.Length > CONST.NAME_MAX_LENGTH ) return false;
            return true;
        }
        public bool ValidateStringAddress(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length > CONST.ADDRESS_MAX_LENGTH) return false;
            return true;
        }
        public bool ValidateIntYob(string s)
        {
            if (int.TryParse(s, out int yob))
            {
                if (yob >= CONST.YOB_MIN && yob <= CONST.YOB_MAX) return true;
            }
            return false;
        }
        public bool ValidateDoubleGpa(string s)
        {
            if (double.TryParse(s.Trim(), out double gpa))
            {
                if (gpa >= CONST.GPA_MIN && gpa <= CONST.GPA_MAX) { return true; }
            }
            return false;
        }

        // ---- Search String ----
        public string ValidateStringSearch(string s, string inputFormat)
        {
            if (string.IsNullOrEmpty(s)) return "UserInputIsNullOrEmpty";
            if (s.Split(',').Length != inputFormat.Split(',').Length) 
                return "UserInput NOT matching with input format.";
            var input = s.ToLower().Split(',');
            var format = inputFormat.ToLower().Split(",");
            for (int i = 0; i < input.Length; i++)
            {
                string ca = format[i];
                switch (ca)
                {
                    case "id":
                        if (string.IsNullOrEmpty(input[i]) || input[i].Length > CONST.ID_MAX_LENGTH) 
                            return $"ID not NULL and format {CONST.ID_FORMAT}";
                        break;
                    case "name":
                        if (string.IsNullOrEmpty(input[i]) || input[i].Length > CONST.NAME_MAX_LENGTH)
                            return $"Name not NULL and max length is {CONST.NAME_MAX_LENGTH}";
                        break;
                    case "address":
                        if (string.IsNullOrEmpty(input[i]) || input[i].Length > CONST.ADDRESS_MAX_LENGTH)
                            return $"Address not NULL and max length is {CONST.ADDRESS_MAX_LENGTH}";
                        break;
                    case "yob":
                        if (!ValidateIntYob(input[i])) 
                            return $"Yob must be positive integer and between {CONST.YOB_MIN} to {CONST.YOB_MAX}.";
                        break;
                    case "gpa":
                        if (!ValidateDoubleGpa(input[i])) 
                            return $"Gpa must be double number and between {CONST.GPA_MIN} to {CONST.GPA_MAX}";
                        break;
                }
            }
            return "yes";
        }
    }
}
