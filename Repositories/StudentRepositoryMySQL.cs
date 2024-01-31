using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    /*
     * Have to rewrite repository class  >>> NG
     * 
     */


    public class StudentRepositoryMySQL
    {
        private List<Student> _students = new();

        public StudentRepositoryMySQL()
        {
            InitData();
        }

        private void InitData()
        {
            //_students.Add(new Student() { Id = "SE1", Name = "An", Address = "Dĩ An", Gpa = 8.8, Yob = 2003 });
            _students.Add(new Student("SE000001", "An", "Dĩ An", 2003, 8.8));
            _students.Add(new Student("SE000002", "Bình", "Bình Dương", 2008, 9.0));
            _students.Add(new Student("CS000005", "Dương", "Tân Bình", 2005, 5.0));
            _students.Add(new Student("SE000004", "Dũng", "Châu Thành", 2006, 5.0));
            _students.Add(new Student("AT000003", "Thành", "Long An", 2000, 8.0));
            _students.Add(new Student("FE000006", "Thinh", "Rach Gia", 2001, 8.2));
            _students.Add(new Student("BE000008", "Xuan", "Long An", 2003, 8.3));
            _students.Add(new Student("ST000007", "Trinh", "Bến tre", 2002, 7.9));
        }

        /// <summary>
        /// Return all students in database
        /// </summary>
        /// <returns>tra ve 1 List</returns>
        public List<Student> GetAll()
        {
            return _students;
        }








    }
}
