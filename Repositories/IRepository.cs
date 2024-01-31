using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /*  
     *  GUI :   IRepository _repo = new StudentRepositoryCSV();
     *          IRepository _repo = new StudentRepositorySqlServer();
     *          IRepository _repo = new StudentRepositoryMySQL();
     *          ... MONGODB...
     *          DEPENDENCY INJECTION
     */
    public interface IRepository<T>
    {
        // no body func but...
        public List<T>? GetAll();
        public bool Add(T item);
        public bool Update(T item);
        public bool Delete(string id);
        public T? GetById(string id);
        public List<T>? Search(string keyword);
        //-------------------------------------------------------------------------------------------------
        List<T>? Search(string? id, string? name, string? address, int? yob, double? gpa, string logicOperator);
        List<Student>? SortingByColName(string columnName);
    }
}
