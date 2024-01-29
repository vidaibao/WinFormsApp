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
            dgvStudentsList.EnableHeadersVisualStyles = false; //
            dgvStudentsList.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            dgvStudentsList.Columns[0].HeaderCell.Style.BackColor = Color.Magenta;
            dgvStudentsList.Columns[1].HeaderCell.Style.BackColor = Color.Yellow;

            // Search optional
            ckbName.Checked = true;
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
            return true;
        }


        private void AddStudent(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton();

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
                    RefreshStudentInfo(st.Id, st.Name, st.Address, st.Yob, st.Gpa);
                    RefreshGrid(_repo.GetAll());
                    //dgvStudentsList.Refresh(); no work
                }
            }
        }


        private void MoveCursorToFormCenter()
        {
            // Calculate the center of the form
            int centerX = Location.X + Width / 2;
            int centerY = Location.Y + Height / 2;

            // Set the cursor's position to the center of the form
            Cursor.Position = new Point(centerX, centerY);
        }

        private void PreventMultipleClickOnButton()
        {
            MoveCursorToFormCenter();
        }




        private bool runningExclusiveProcess = false;
        private void AddStudent01(object sender, EventArgs e)
        {
            if (!runningExclusiveProcess)
            {
                runningExclusiveProcess = true;
                btnAdd.Enabled = false;

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
                        RefreshStudentInfo(st.Id, st.Name, st.Address, st.Yob, st.Gpa);
                        RefreshGrid(_repo.GetAll());
                        //dgvStudentsList.Refresh(); no work
                    }
                }



                // If your task is synchronous, then undo your flag here:
                runningExclusiveProcess = false;
                btnAdd.Enabled = true;
            }
        }


        private void AddStudent00(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            //btnAdd.Click -= AddStudent;
            try
            {
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
                        RefreshStudentInfo(st.Id, st.Name, st.Address, st.Yob, st.Gpa);
                        RefreshGrid();
                        //dgvStudentsList.Refresh(); no work
                    }
                }
                // Simulate a delay (replace this with your actual operation)
                //System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("An error occurred: " + ex.Message);
                txtMsg.Text = ex.Message;
            }
            finally
            {
                btnAdd.Enabled = true;
                btnAdd.Click += AddStudent;
            }

        }

        private void RefreshGrid(List<Student>? _list)
        {
            dgvStudentsList.DataSource = null;
            dgvStudentsList.DataSource = _list;
        }

        private void RefreshStudentInfo(string? id, string? name, string? address, int yob, double gpa)
        {
            txtID.Text = id; txtName.Text = name; txtAddress.Text = address; txtYob.Text = yob.ToString(); txtGpa.Text = gpa.ToString();
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
                // Parse YOB (Year of Birth) and GPA to int and double respectively
                int yob;
                double gpa;

                if (int.TryParse(row.Cells[3].Value.ToString(), out yob) &&
                    double.TryParse(row.Cells[4].Value.ToString(), out gpa))
                {
                    // Call RefreshStudentInfo with the parsed values
                    RefreshStudentInfo(
                        row.Cells[0].Value.ToString(),
                        row.Cells["Name"].Value.ToString(),
                        row.Cells["Address"].Value.ToString(),
                        yob,
                        gpa
                    );
                }
                else
                {
                    // Handle parsing failure if needed
                    //MessageBox.Show("Failed to parse Year of Birth (YOB) or GPA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMsg.Text = "Failed to parse Year of Birth (YOB) or GPA.";
                }
            }
        }

        private void UpdateStudent(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton();

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
            PreventMultipleClickOnButton();

            if (_repo.Delete(txtID.Text))
            {
                txtMsg.Text = $"Student ID = {txtID.Text} deleted successfully.";
                //dgvStudentsList.Refresh();  // worked //
                RefreshGrid(_repo.GetAll());
                ClearStudentInfo();
            }
            else
            {
                txtMsg.Text = "Failed to delete student.";
            }
        }

        private void SearchStudents(object sender, EventArgs e)
        {
            PreventMultipleClickOnButton();
            var result = _repo.FindStudents("Chich", 1800, 4.0);
            dgvSearchResult.DataSource = result;
        }

        private void SortByColumnName(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Check if the clicked area is a column header
            if (e.RowIndex == -1 && e.Button == MouseButtons.Left)
            {
                // Get the column name
                string columnName = dgvStudentsList.Columns[e.ColumnIndex].Name;

                // Now you have the column name, you can perform sorting or any other operations
                RefreshGrid(_repo.SortingByColName(columnName));
            }
        }
    }
}