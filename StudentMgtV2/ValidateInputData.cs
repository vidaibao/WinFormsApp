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

            return true;
        }
        public bool ValidateDoubleGpa(string s)
        {

            return true;
        }
    }
}
