using Repositories;
using System;
using System.Windows.Forms;

namespace StudentMgtV2
{
    public partial class frmStudentsList : Form
    {

        private StudentRepositorySqlserver _repo; // not new() here b/c ...


        public frmStudentsList()
        {
            InitializeComponent();
        }

        private void frmStudentsList_Load(object sender, EventArgs e)
        {
            _repo = new StudentRepositorySqlserver();
            var students = _repo.GetAll();
            dgvStudentsList.DataSource = students;

            // Adjust dgv header
            AdjustDGVHeader();

            // PlaceHolderText
            InitInputText();

            // Search optional
            ckbName.Checked = true;
            rbAND.Checked = true;
        }

        private void InitInputText()
        {
            //txtID.PlaceholderText = "SA123456"; // not work with read-only true
            txtName.PlaceholderText = "name";
            txtAddress.PlaceholderText = "address";
            txtYob.PlaceholderText = "1980";
            txtGpa.PlaceholderText = "5.0";

            txtSearch.PlaceholderText = "ID,Name,Address,Yob,Gpa;sa123,name,address,1980,5.0";
        }

        private void AdjustDGVHeader()
        {
            dgvStudentsList.EnableHeadersVisualStyles = false; //
            dgvStudentsList.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            dgvStudentsList.Columns[0].HeaderCell.Style.BackColor = Color.Magenta;
            dgvStudentsList.Columns[1].HeaderCell.Style.BackColor = Color.Yellow;
        }

        private bool ValidateInputData()
        {
            ValidateInputData userInput = new ValidateInputData();
            // ID is read-only and unique, auto generate
            txtName.Text = txtName.Text.Trim();
            if (!userInput.ValidateStringName(txtName.Text))
            {
                txtMsg.Text = "Name must not be null or empty and should be less than 20 characters in length.";
                return false;
            }
            // 
            txtAddress.Text = txtAddress.Text.Trim();
            if (!userInput.ValidateStringAddress(txtAddress.Text))
            {
                txtMsg.Text = "Name must not be null or empty and should be less than 50 characters in length.";
                return false;
            }
            // 
            txtYob.Text = txtYob.Text.Trim();
            if (!userInput.ValidateIntYob(txtYob.Text))
            {
                txtMsg.Text = "Year of Birth must be integer and between 1980 to 2020.";
                return false;
            }
            // 
            txtGpa.Text = txtGpa.Text.Trim();
            if (!userInput.ValidateDoubleGpa(txtGpa.Text))
            {
                txtMsg.Text = "Gpa number must be between 5.0 to 10.0";
                return false;
            }
            return true;
        }


        private void AddStudent(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton(btnAdd.Location);

            if (!ValidateInputData()) return;

            string res = _repo.Add(txtName.Text, txtAddress.Text, int.Parse(txtYob.Text), double.Parse(txtGpa.Text));
            if (string.IsNullOrEmpty(res))
            {
                txtMsg.Text = "Failed to add student.";
            }
            else
            {
                txtMsg.Text = $"Student id = {res} added successfully.";
                var st = _repo.FindAStudentByID(res);
                if (st != null)
                {
                    ViewAStudentInfo(st.Id, st.Name, st.Address, st.Yob.ToString(), st.Gpa.ToString());
                    RefreshGrid(_repo.GetAll());
                    //dgvStudentsList.Refresh(); no work
                }
            }
        }


        private void MoveCursorOutOfItem(Point itemLocation)
        {
            int X = Location.X + itemLocation.X; int Y = Location.Y + itemLocation.Y;
            Cursor.Position = new Point(X, Y);
        }
        private void MoveCursorToFormCenter()
        {
            // Calculate the center of the form
            int centerX = Location.X + Width / 2;
            int centerY = Location.Y + Height / 2;

            // Set the cursor's position to the center of the form
            Cursor.Position = new Point(centerX, centerY);
        }

        private void PreventMultipleClickOnButton(Point itemLocation)
        {
            //MoveCursorToFormCenter();
            MoveCursorOutOfItem(itemLocation);
        }





        private void RefreshGrid(List<Student> _list)
        {
            if (_list == null) return;
            dgvStudentsList.DataSource = null;
            dgvStudentsList.DataSource = _list;
        }

        private void ViewAStudentInfo(string? id, string? name, string? address, string? yob, string? gpa)
        {
            txtID.Text = id; txtName.Text = name; txtAddress.Text = address; txtYob.Text = yob; txtGpa.Text = gpa;
        }

        private void ClearStudentInfo()
        {
            txtID.Text = ""; txtName.Text = ""; txtAddress.Text = ""; txtYob.Text = ""; txtGpa.Text = "";
        }

        private void SelectedRows(object sender, EventArgs e)
        {
            if (dgvStudentsList.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStudentsList.SelectedRows[0];
                // Call RefreshStudentInfo with the parsed values
                ViewAStudentInfo(
                    row.Cells[0].Value.ToString(),
                    row.Cells["Name"].Value.ToString(),
                    row.Cells["Address"].Value.ToString(),
                    row.Cells[3].Value.ToString(),
                    row.Cells[4].Value.ToString()
                );

            }
        }

        private void UpdateStudent(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton(btnUpdate.Location);

            if (!ValidateInputData()) return;

            if (_repo.Update(txtID.Text, txtName.Text, txtAddress.Text
                , int.Parse(txtYob.Text), double.Parse(txtGpa.Text)))
            {
                txtMsg.Text = $"Student ID = {txtID.Text} updated successfully.";
                dgvStudentsList.Refresh();  // worked
                //RefreshGrid();              // grid auto refresh ??? but not binding
            }
            else
            {
                txtMsg.Text = "Failed to update student.";
            }
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton(btnDelete.Location);
            // Get the index of the selected row
            int selectedIndex = dgvStudentsList.SelectedRows[0].Index;

            if (_repo.Delete(txtID.Text))
            {
                txtMsg.Text = $"Student ID = {txtID.Text} deleted successfully.";
                //dgvStudentsList.Refresh();  // worked //
                RefreshGrid(_repo.GetAll());
                ClearStudentInfo();
                // Determine the index of the next row to select
                int nextIndex = selectedIndex;
                // If the nextIndex is out of bounds, select the last row
                if (nextIndex >= dgvStudentsList.Rows.Count)
                {
                    nextIndex = dgvStudentsList.Rows.Count - 1;
                }
                // Select the next row programmatically
                if (nextIndex >= 0)
                {
                    dgvStudentsList.Rows[nextIndex].Selected = true;
                    // Set the index of the first row to be displayed
                    dgvStudentsList.FirstDisplayedScrollingRowIndex = nextIndex;
                }
            }
            else
            {
                txtMsg.Text = "Failed to delete student.";
            }
        }

        private void SearchStudents(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton(groupBox1.Location);


            var result = _repo.SearchStudents(
                 id:        s[0].Equals("null") ? null : s[0],
                 name:      s[1].Equals("null") ? null : s[1],
                 address:   s[2].Equals("null") ? null : s[2],
                 yob:       s[3].Equals("null") ? null : int.Parse(s[3]),
                 gpa:       s[4].Equals("null") ? null : double.Parse(s[4]),
                 logicOperator: op
                 );
            dgvSearchResult.DataSource = result;
        }

        private void SortByColumnName(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Check if the clicked area is a column header
            if (e.RowIndex == -1 && e.Button == MouseButtons.Left)
            {
                // Get the column name
                string columnName = dgvStudentsList.Columns[e.ColumnIndex].Name;

                // Set the back color of the cell
                // dgvStudentsList.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dgvStudentsList.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.Azure;

                // Now you have the column name, you can perform sorting or any other operations
                RefreshGrid(_repo.SortingByColName(columnName));
            }
        }

        // txtSearch get Focus
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            // 
            //var ss = txtSearch.PlaceholderText.Split(';');
            //txtMsg.Text = $"{ss[0]}; Example: {ss[1]}";

            txtMsg.Text = BuildSearchQuery();
        }

        private string[] s;
        private string op;
        private string inputFormat;
        private string BuildSearchQuery()
        {
            s = new string[5]; for (int i = 0; i < s.Length; i++) s[i] = "null";
            op = rbAND.Checked ? "AND" : " OR";

            if (ckbID.Checked) s[0] = "id";
            if (ckbName.Checked) s[1] = "name";
            if (ckbAddress.Checked) s[2] = "address";
            if (ckbYob.Checked) s[3] = "yob";
            if (ckbGpa.Checked) s[4] = "gpa";
            string defaultS = "";
            if (s.Where(x => x != "null").Count() <= 0)
            {
                s[1] = "name";
                defaultS = "Your default query is name. ";
            }
            inputFormat = string.Join(",", s.Where(x => x != "null"));
            return $"{defaultS}Please input: {inputFormat}\t(Query: {string.Join(",", s)},{op})";
        }

        private void ckbName_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void ckbID_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void ckbAddress_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void ckbYob_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void ckbGpa_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void rbAND_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }

        private void rbOR_CheckedChanged(object sender, EventArgs e)
        {
            txtMsg.Text = BuildSearchQuery();
        }
    }
}



//not work
//private bool runningExclusiveProcess = false;
//private void AddStudent01(object sender, EventArgs e)
//{
//    if (!runningExclusiveProcess)
//    {
//        runningExclusiveProcess = true;
//        btnAdd.Enabled = false;

//        if (!ValidateInputData()) return;

//        string res = _repo.Add(txtName.Text, txtAddress.Text, int.Parse(txtYob.Text), double.Parse(txtGpa.Text));
//        if (string.IsNullOrEmpty(res))
//        {
//            txtMsg.Text = "Failed to add student.";
//        }
//        else
//        {
//            txtMsg.Text = $"Student id = {res} added successfully.";
//            var st = _repo.FindAStudentByID(res);
//            if (st != null)
//            {
//                ViewAStudentInfo(st.Id, st.Name, st.Address, st.Yob, st.Gpa);
//                RefreshGrid(_repo.GetAll());
//                //dgvStudentsList.Refresh(); no work
//            }
//        }



//        // If your task is synchronous, then undo your flag here:
//        runningExclusiveProcess = false;
//        btnAdd.Enabled = true;
//    }
//}

//// Not work
//private void AddStudent00(object sender, EventArgs e)
//{
//    btnAdd.Enabled = false;
//    //btnAdd.Click -= AddStudent;
//    try
//    {
//        if (!ValidateInputData()) return;

//        string res = _repo.Add(txtName.Text, txtAddress.Text, int.Parse(txtYob.Text), double.Parse(txtGpa.Text));
//        if (string.IsNullOrEmpty(res))
//        {
//            txtMsg.Text = "Failed to add student.";
//        }
//        else
//        {
//            txtMsg.Text = $"Student id = {res} added successfully.";
//            var st = _repo.FindAStudentByID(res);
//            if (st != null)
//            {
//                ViewAStudentInfo(st.Id, st.Name, st.Address, st.Yob, st.Gpa);
//                RefreshGrid(_repo.GetAll());
//                //dgvStudentsList.Refresh(); no work
//            }
//        }
//        // Simulate a delay (replace this with your actual operation)
//        //System.Threading.Thread.Sleep(2000);
//    }
//    catch (Exception ex)
//    {
//        //MessageBox.Show("An error occurred: " + ex.Message);
//        txtMsg.Text = ex.Message;
//    }
//    finally
//    {
//        btnAdd.Enabled = true;
//        btnAdd.Click += AddStudent;
//    }

//}