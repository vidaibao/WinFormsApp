using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMgtV3
{
    public class CONST
    {
        public const int    ID_MAX_LENGTH = 8;
        public const int    NAME_MAX_LENGTH = 20;
        public const int    ADDRESS_MAX_LENGTH = 50;
        public const int    YOB_MIN = 1980;
        public const int    YOB_MAX = 2020;
        public const double GPA_MIN = 5.0;
        public const double GPA_MAX = 10.0;

        public const string ID_FORMAT = "AA123456";
        public const int    STUDENT_FIELDS = 5;
        // The const keyword in C# can only be used with primitive types or strings
        //public const string[] exam = new string[] { "AA123***","","","","" }; 
        // readonly fields can only be initialized at the declaration or within the constructor of the class
        public static readonly string[] STUDENT_KEY_EXAMPLE = new string[] { "AA123***", "an", "fsdf**", "1980", "5.0" }; // Dict is better ???

    }
}
