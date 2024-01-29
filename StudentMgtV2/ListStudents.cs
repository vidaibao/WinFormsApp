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
        }

        private bool ValidateInputData()
        {
            ValidateInputData userInput = new ValidateInputData();
            // ID is read-only and unique, auto generate
            txtName.Text = txtName.Text.Trim();
            if (!userInput.ValidateStringName(txtName.Text))
            {
                lblMsg.Text = "Name must not be null or empty and should be between 6 and 20 characters in length.";
                return false;
            }
            return true;
        }

        private void AddStudent(object sender, EventArgs e)
        {
            if (!ValidateInputData()) return;

            string res = _repo.Add(txtName.Text, txtAddress.Text, int.Parse(txtYob.Text), double.Parse(txtGpa.Text));
            if (string.IsNullOrEmpty(res))
            {
                lblMsg.Text = "Failed to add student.";
            }
            else
            {
                lblMsg.Text = $"Student id = {res} added successfully.";
            }
        }
    }
}