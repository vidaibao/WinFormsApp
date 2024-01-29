namespace StudentMgtV2
{
    partial class frmStudentsList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtID = new TextBox();
            label3 = new Label();
            txtName = new TextBox();
            label4 = new Label();
            txtAddress = new TextBox();
            label5 = new Label();
            txtYob = new TextBox();
            label6 = new Label();
            txtGpa = new TextBox();
            txtSearch = new TextBox();
            groupBox1 = new GroupBox();
            button4 = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dgvStudentsList = new DataGridView();
            dgvSearchResult = new DataGridView();
            txtMsg = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudentsList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSearchResult).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Mincho", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Highlight;
            label1.Location = new Point(249, 10);
            label1.Name = "label1";
            label1.Size = new Size(282, 41);
            label1.TabIndex = 0;
            label1.Text = "学生管理システム";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 69);
            label2.Name = "label2";
            label2.Size = new Size(18, 15);
            label2.TabIndex = 1;
            label2.Text = "ID";
            // 
            // txtID
            // 
            txtID.Location = new Point(95, 66);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(120, 23);
            txtID.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 98);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 1;
            label3.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(95, 95);
            txtName.Name = "txtName";
            txtName.Size = new Size(120, 23);
            txtName.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 127);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 1;
            label4.Text = "Address";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(95, 124);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(120, 23);
            txtAddress.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 156);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 1;
            label5.Text = "Yob";
            // 
            // txtYob
            // 
            txtYob.Location = new Point(95, 153);
            txtYob.Name = "txtYob";
            txtYob.Size = new Size(120, 23);
            txtYob.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(27, 185);
            label6.Name = "label6";
            label6.Size = new Size(28, 15);
            label6.TabIndex = 1;
            label6.Text = "Gpa";
            // 
            // txtGpa
            // 
            txtGpa.Location = new Point(95, 182);
            txtGpa.Name = "txtGpa";
            txtGpa.Size = new Size(120, 23);
            txtGpa.TabIndex = 4;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(72, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(110, 23);
            txtSearch.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(txtSearch);
            groupBox1.Location = new Point(27, 266);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(192, 50);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search";
            // 
            // button4
            // 
            button4.Location = new Point(6, 18);
            button4.Name = "button4";
            button4.Size = new Size(60, 26);
            button4.TabIndex = 1;
            button4.Text = "Search";
            button4.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(27, 222);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(60, 26);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += AddStudent;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(93, 222);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(60, 26);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += UpdateStudent;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(159, 222);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 26);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += DeleteStudent;
            // 
            // dgvStudentsList
            // 
            dgvStudentsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStudentsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudentsList.Location = new Point(249, 66);
            dgvStudentsList.Name = "dgvStudentsList";
            dgvStudentsList.RowTemplate.Height = 25;
            dgvStudentsList.Size = new Size(407, 193);
            dgvStudentsList.TabIndex = 8;
            dgvStudentsList.SelectionChanged += SelectedRows;
            // 
            // dgvSearchResult
            // 
            dgvSearchResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSearchResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSearchResult.Location = new Point(249, 266);
            dgvSearchResult.Name = "dgvSearchResult";
            dgvSearchResult.RowTemplate.Height = 25;
            dgvSearchResult.Size = new Size(407, 65);
            dgvSearchResult.TabIndex = 10;
            // 
            // txtMsg
            // 
            txtMsg.BorderStyle = BorderStyle.None;
            txtMsg.Location = new Point(40, 410);
            txtMsg.Name = "txtMsg";
            txtMsg.PlaceholderText = "Status...";
            txtMsg.ReadOnly = true;
            txtMsg.Size = new Size(616, 16);
            txtMsg.TabIndex = 11;
            // 
            // frmStudentsList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtMsg);
            Controls.Add(dgvSearchResult);
            Controls.Add(dgvStudentsList);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(groupBox1);
            Controls.Add(txtGpa);
            Controls.Add(label6);
            Controls.Add(txtYob);
            Controls.Add(label5);
            Controls.Add(txtAddress);
            Controls.Add(label4);
            Controls.Add(txtName);
            Controls.Add(label3);
            Controls.Add(txtID);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmStudentsList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "List of Students";
            Load += frmStudentsList_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudentsList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSearchResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtID;
        private Label label3;
        private TextBox txtName;
        private Label label4;
        private TextBox txtAddress;
        private Label label5;
        private TextBox txtYob;
        private Label label6;
        private TextBox txtGpa;
        private TextBox txtSearch;
        private GroupBox groupBox1;
        private Button button4;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private DataGridView dgvStudentsList;
        private DataGridView dgvSearchResult;
        private TextBox txtMsg;
    }
}