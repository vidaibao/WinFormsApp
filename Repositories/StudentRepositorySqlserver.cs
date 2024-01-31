using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories  // 
{
    public class StudentRepositorySqlServer : IRepository<Student>
    {
        private List<Student> _students = new();

        public StudentRepositorySqlServer()
        {
            InitData();
        }


        // teporary data on RAM
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

        bool IRepository<Student>.Add(Student st)
        {
            var x = ((IRepository<Student>)this).GetById(st.Id);
            if (x == null) return false;
            _students.Add(x);
            // check
            x = ((IRepository<Student>)this).GetById(st.Id);
            return (x == null) ? false : true;
        }

        bool IRepository<Student>.Delete(string id)
        {
            var x = ((IRepository<Student>)this).GetById(id);
            if (x == null) return false;
            _students.Remove(x);
            // check
            x = ((IRepository<Student>)this).GetById(id);
            return (x == null) ? true : false;
        }

        List<Student>? IRepository<Student>.GetAll()
        {
            return _students;
        }

        Student? IRepository<Student>.GetById(string id)
        {
            return _students.FirstOrDefault(x => x.Id.Equals(id));
        }

        List<Student>? IRepository<Student>.Search(string keyword)
        {
            return _students.Where(x => x.Name.ToLower().Equals(keyword.ToLower())).ToList();
        }

        // Method to search students based on criteria and logic operator
        List<Student>? IRepository<Student>.Search(string? id = null, string? name = null, string? address = null, int? yob = null, double? gpa = null, string logicOperator = "AND")
        {
            // Start with all students
            IEnumerable<Student> query = _students;

            // Apply filters based on provided criteria
            if (id != null)
                query = query.Where(s => s.Id.ToLower().Contains(id.ToLower()));
            if (name != null)
                query = query.Where(s => s.Name.ToLower().Contains(name.ToLower()));
            if (address != null)
                query = query.Where(s => s.Address.ToLower().Contains(address.ToLower()));
            if (yob != null)
                query = query.Where(s => s.Yob == yob);
            if (gpa != null)
                query = query.Where(s => s.Gpa == gpa);

            // Return the result based on the logic operator
            return logicOperator == "AND" ? query.ToList() : _students.Intersect(query).ToList();
        }

        //-------------------------------------------------------------------------------------------------
        bool IRepository<Student>.Update(Student st)
        {
            var x = ((IRepository<Student>)this).GetById(st.Id);
            if (x == null) return false;
            x.Name = st.Name;
            x.Address = st.Address;
            x.Yob = st.Yob;
            x.Gpa = st.Gpa;
            // 
            x = ((IRepository<Student>)this).GetById(st.Id);
            if (x == null || x.Id != st.Id || x.Name != st.Name || x.Address != st.Address || x.Yob != st.Yob || x.Gpa != st.Gpa ) return false;
            return true;
        }

        
        public List<Student> SortingByColName(string colName)
        {
            colName = colName.ToUpper();
            if (colName.Equals("ID")) return _students.OrderBy(x => x.Id).ToList();
            if (colName.Equals("NAME")) return _students.OrderBy(x => x.Name).ToList();
            if (colName.Equals("ADDRESS")) return _students.OrderBy(x => x.Address).ToList();
            if (colName.Equals("YOB")) return _students.OrderBy(x => x.Yob).ToList();
            if (colName.Equals("GPA")) return _students.OrderBy(x => x.Gpa).ToList();
            return _students;
        }

    }
}
