using Repositories;

namespace StudentTCData
{
    internal class Program
    {
        static Random random = new Random();


        static void Main(string[] args)
        {
            int numStudents = 1200;
            var students = GenerateStudents(numStudents);
            WriteStudentsToCsv(students, "students_TC01.csv");
            Console.WriteLine($"{numStudents} students generated and saved to students.csv");
        }

        static string GenerateStudentId()
        {
            string prefix = new string[] { "SE", "CS", "AT", "FE", "BE", "ST", "RD", "SW" }[random.Next(8)];
            string suffix = random.Next(1, 999999).ToString("D6");
            return prefix + suffix;
        }

        static string GenerateRandomName()
        {
            string tenVietNam = new string[] { 
                "An", "Bình", "Cường", "Dung", "Đức", "Lan", "Hằng", "Hải", "Thanh", "Trường",
                "Mai", "Khánh", "Ngọc", "Phú", "Huệ", "Long", "Hương", "Đạt", "Hạnh", "Tú", "Tí Kẹo",
                "Hiệp", "Minh", "Thúy", "Đức", "Anh", "Dũng", "Hà", "Tâm", "Loan", "Sơn", "Chích Bông",
                "Hồng", "Hùng", "Thúy", "Trường", "Lan Anh", "Thị Nhung", "Kim Ngân", "Thị Hạnh", "Thiên Kim",
                "Thùy Dương", "Hồng Nhung", "Ngọc Trâm", "Ngọc Ánh", "Diễm Hương", "Mai Phương", "Thiên Kim", "Thiên Trang", "Hạnh Phúc", "Tuyết Nhung",
                "Lan Phương", "Thùy Trang", "Thiên Di", "Thu Hằng", "Hà My", "Thùy Linh", "Hương Trà", "Hồng Anh", "Trần Thảo", "Mai Anh",
                "Lệ Hằng", "Thúy An", "Hạnh Nguyệt", "Bích Ngọc", "Trần Thanh", "Diễm Trinh", "Mai Trang", "Tú Uyên", "Thiên Kim", "Thiên Thanh",
                "Ngọc Lan", "Thiên Di", "Trúc Ly", "Kim Oanh", "Thúy Anh", "Tú Quyên", "Phương Mai", "Kim Loan", "Hồng Nhung", "Thị Ngọc",
                "Hương Giang", "Hồng Tuyết", "Mai Hương", "Thiên Mai", "Chích Chòe" }[random.Next(86)]; ; // 33+50+3
            return tenVietNam;
            // Bạn có thể thêm các tên khác vào đây nếu cần};
            // int nameLength = random.Next(6, 9);
            //string name = "";
            //for (int i = 0; i < nameLength; i++)
            //{
            //    name += (char)random.Next('a', 'z' + 1);
            //}
            //return char.ToUpper(name[0]) + name.Substring(1);
        }

        static string GenerateRandomAddress()
        {
            string add = new string[] { "Hà Nội", "Hà Giang", "Cao Bằng", "Bắc Kạn", "Tuyên Quang", "Lào Cai", "Ðiện Biên", "Lai Châu", "Sơn La", "Yên Bái", "Hoà Bình", "Thái Nguyên", "Lạng Son", "Quảng Ninh", "Bắc Giang", "Phú Thọ", "Vĩnh Phúc", "Bắc Ninh", "Hải Dương", "Hải Phòng", "Hưng Yên", "Thái Bình", "Hà Nam", "Nam Ðịnh", "Ninh Bình", "Thanh Hóa", "Nghệ An", "Hà Tĩnh", "Quảng Bình", "Quảng Trị", "Thừa Thiên Huế", "Ðà Nẵng", "Quảng Nam", "Quảng Ngãi", "Bình Ðịnh", "Phú Yên", "Khánh Hòa", "Ninh Thuận", "Bình Thuận", "Kon Tum", "Gia Lai", "Ðắk Lắk", "Ðắk Nông", "Lâm Ðồng", "Bình Phước", "Tây Ninh", "Bình Dương", "Ðồng Nai", "Bà Rịa - Vũng Tàu", "Hồ Chí Minh", "Long An", "Tiền Giang", "Bến Tre", "Trà Vinh", "Vĩnh Long", "Ðồng Tháp", "An Giang", "Kiên Giang", "Cần Thơ", "Hậu Giang", "Sóc Trăng", "Bạc Liêu", "Cà Mau" }[random.Next(63)];
            return add;
            //int addressLength = random.Next(5, 16);
            //string address = "";
            //for (int i = 0; i < addressLength; i++)
            //{
            //    address += (char)random.Next('a', 'z' + 1);
            //}
            //return char.ToUpper(address[0]) + address.Substring(1);
        }

        static List<Student> GenerateStudents(int numStudents)
        {
            var students = new List<Student>();
            for (int i = 0; i < numStudents; i++)
            {
                students.Add(new Student(GenerateStudentId(), GenerateRandomName(), GenerateRandomAddress(),
                    random.Next(2000, 2008), Math.Round(random.NextDouble() * (10 - 5) + 5, 1)));
                //{
                //    Id = GenerateStudentId(),
                //    Name = GenerateRandomName(),
                //    Address = GenerateRandomAddress(),
                //    Yob = random.Next(2000, 2021),
                //    Gpa = Math.Round(random.NextDouble() * (10 - 5) + 5, 1)
                //});
            }
            return students;
        }

        static void WriteStudentsToCsv(List<Student> students, string filename)
        {
            // the CSV file in the same directory as the executable file (.exe) 
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(directoryPath, filename);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ID,Name,Address,Year of Birth,GPA");
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Id},{student.Name},{student.Address},{student.Yob},{student.Gpa}");
                }
            }
        }

    }
}