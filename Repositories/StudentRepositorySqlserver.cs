using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Repositories
{
    public class StudentRepositorySqlserver
    {
        private List<Student> _students = new();
        static Random random = new Random();

        
        public StudentRepositorySqlserver()
        {
            // Call connect to Sql server OR initialize data on RAM
            //InitData();
            LoadDataFromFile("students_TC01.csv");
        }

        private void LoadDataFromFile(string filename)
        {
            try
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(directoryPath, filename); // 
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            _students.Add(new Student(parts[0], parts[1], parts[2], int.Parse(parts[3]), double.Parse(parts[4])));
                        }
                    }
                    sr.Close();
                    _students.Add(new Student("SS321654", "Chich Choe", "Sai Gon", 2010, 8.8));
                    _students.Add(new Student("SS654321", "Chich Bong", "Sai Gon", 2014, 8.8));
                    _students.Add(new Student("SS432165", "Ti Keo", "Sai Gon", 1988, 8.8));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data from file: " + ex.Message);
            }
        }

        private void InitData()
        {
            //_students.Add(new Student() { Id = "SE1", Name = "An", Address = "Dĩ An", Gpa = 8.8, Yob = 2003 });
            _students.Add(new Student("SE000001", "An", "Dĩ An", 2003, 8.8));
            _students.Add(new Student("SE000002","Bình","Bình Dương", 2008,9.0));
            _students.Add(new Student("CS000005", "Dương", "Tân Bình", 2005, 5.0));
            _students.Add(new Student("SE000004", "Dũng", "Châu Thành", 2006,5.0));
            _students.Add(new Student("AT000003","Thành", "Long An", 2000, 8.0));
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

        /// <summary>
        /// Generate new ID then adding new student.
        /// If added successful then return the new id.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="yob"></param>
        /// <param name="gpa"></param>
        /// <returns>string.Empty if failed</returns>
        public string Add(string name, string address, int yob, double gpa)
        {
            string id = ValidateNewStudentId();
            // create new Student  -  if already exits
            //if (_students.Any(x => x.Id.Equals(id))) return false;

            var student = new Student(id, name, address, yob, gpa);
            _students.Add(student);
            if (_students.Any(x => x.Id.Equals(id))) return id; // successful add new
            return string.Empty;
        }

        public bool Update(string id, string name, string address, int yob, double gpa)
        {
            // Find the item with the specified ID and update its name using LINQ (FirstOrDefault == Where return 1 element)
            var itemToUpdate = _students.FirstOrDefault(x => x.Id.Equals(id)); // pointer to found student
            if (itemToUpdate == null) return false; // item with ID not found
            
            itemToUpdate.Name = name;
            itemToUpdate.Address = address;
            itemToUpdate.Yob = yob;
            itemToUpdate.Gpa = gpa;
            // 
            var updatedItem = _students.FirstOrDefault(x => x.Id.Equals(id));
            if (updatedItem != null)
            {
                if (updatedItem.Name.Equals(name) && updatedItem.Address.Equals(address) 
                    && updatedItem.Yob == yob && updatedItem.Gpa == gpa)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Delete(string id)
        {
            var itemToDelete = _students.FirstOrDefault(x => x.Id.Equals(id));
            if (itemToDelete == null) return false;
            _students.Remove(itemToDelete);
            //
            var deletedItem = _students.FirstOrDefault(x => x.Id.Equals(id));
            return (deletedItem == null);
        }


        public string ValidateNewStudentId()
        {
            string newID;
            bool validate = false;
            do
            {
                newID = GenerateStudentId();
                var itemExits = _students.FirstOrDefault(x => x.Id.Equals(newID));
                if (itemExits == null) validate = true;
            } while (!validate);
            return newID;
        }

        // 1 more in StudentTCData
        private string GenerateStudentId()
        {
            string prefix = new string[] { "SE", "CS", "AT", "FE", "BE", "ST", "RD", "SW" }[random.Next(8)];
            string suffix = random.Next(1, 999999).ToString("D6");
            return prefix + suffix;
        }

        public Student? FindAStudentByID(string id)
        {
            return _students.FirstOrDefault(x => x.Id.Equals(id));
        }

        //public List<Student> FindStudents(string key) =>   _students.Where(x => x.Name.Contains(key) || x.Address.Contains(key)).ToList();
        //public List<Student> FindStudents(int key) =>   _students.Where(x => x.Yob == key).ToList();
        //public List<Student> FindStudents(double key) => _students.Where(x => x.Gpa == key).ToList();

        public List<Student> FindStudents(string name = "An", int yob = 2000, double gpa = 8.0)
        {
            var query = _students.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name) || x.Address.Contains(name));
            }
            if (yob >= 1990 && yob <= 2020)
            {
                query = query.Where(x => x.Yob == yob);
            }
            if (gpa >= 5.0 && gpa <= 10.0)
            {
                query = query.Where(x => x.Gpa == gpa);
            }
            return query.ToList();
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

        /* Enumeration for logic operators
        public enum LogicOperator
        {
            AND,
            OR
        }
        */

        // Method to search students based on criteria and logic operator
        public List<Student> SearchStudents(string? id = null, string? name = null, string? address = null, int? yob = null, double? gpa = null, string logicOperator = "AND")
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


    }
}