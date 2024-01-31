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
    }
}
