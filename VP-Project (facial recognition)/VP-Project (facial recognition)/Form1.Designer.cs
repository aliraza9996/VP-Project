namespace VP_Project__facial_recognition_
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnMarkAttendance = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnViewStudents = new System.Windows.Forms.Button();
            this.btnDeleteAttendance = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnImportAttendance = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMarkAttendance
            // 
            this.btnMarkAttendance.BackColor = System.Drawing.Color.White;
            this.btnMarkAttendance.FlatAppearance.BorderSize = 0;
            this.btnMarkAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarkAttendance.ImageIndex = 6;
            this.btnMarkAttendance.ImageList = this.imageList1;
            this.btnMarkAttendance.Location = new System.Drawing.Point(74, 53);
            this.btnMarkAttendance.Name = "btnMarkAttendance";
            this.btnMarkAttendance.Size = new System.Drawing.Size(145, 138);
            this.btnMarkAttendance.TabIndex = 1;
            this.btnMarkAttendance.UseVisualStyleBackColor = false;
            this.btnMarkAttendance.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "addstudent.png");
            this.imageList1.Images.SetKeyName(1, "delete attendance.png");
            this.imageList1.Images.SetKeyName(2, "deletestudent.png");
            this.imageList1.Images.SetKeyName(3, "download (1).png");
            this.imageList1.Images.SetKeyName(4, "education_add-512.png");
            this.imageList1.Images.SetKeyName(5, "import.jpg");
            this.imageList1.Images.SetKeyName(6, "markattendance.png");
            this.imageList1.Images.SetKeyName(7, "scan.png");
            this.imageList1.Images.SetKeyName(8, "view attendance.png");
            this.imageList1.Images.SetKeyName(9, "viewstudents.jpg");
            this.imageList1.Images.SetKeyName(10, "exit-1806557-1533018.png");
            this.imageList1.Images.SetKeyName(11, "view.png");
            this.imageList1.Images.SetKeyName(12, "addstudent.png");
            this.imageList1.Images.SetKeyName(13, "images.png");
            this.imageList1.Images.SetKeyName(14, "63-512.png");
            this.imageList1.Images.SetKeyName(15, "delete attendance.png");
            this.imageList1.Images.SetKeyName(16, "exit.png");
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 11;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(452, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 138);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.FlatAppearance.BorderSize = 0;
            this.btnAddStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddStudent.ImageIndex = 12;
            this.btnAddStudent.ImageList = this.imageList1;
            this.btnAddStudent.Location = new System.Drawing.Point(74, 234);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(145, 138);
            this.btnAddStudent.TabIndex = 2;
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnViewStudents
            // 
            this.btnViewStudents.FlatAppearance.BorderSize = 0;
            this.btnViewStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewStudents.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewStudents.ImageIndex = 9;
            this.btnViewStudents.ImageList = this.imageList1;
            this.btnViewStudents.Location = new System.Drawing.Point(454, 234);
            this.btnViewStudents.Name = "btnViewStudents";
            this.btnViewStudents.Size = new System.Drawing.Size(145, 138);
            this.btnViewStudents.TabIndex = 3;
            this.btnViewStudents.UseVisualStyleBackColor = true;
            this.btnViewStudents.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnDeleteAttendance
            // 
            this.btnDeleteAttendance.FlatAppearance.BorderSize = 0;
            this.btnDeleteAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAttendance.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDeleteAttendance.ImageIndex = 15;
            this.btnDeleteAttendance.ImageList = this.imageList1;
            this.btnDeleteAttendance.Location = new System.Drawing.Point(263, 59);
            this.btnDeleteAttendance.Name = "btnDeleteAttendance";
            this.btnDeleteAttendance.Size = new System.Drawing.Size(145, 115);
            this.btnDeleteAttendance.TabIndex = 4;
            this.btnDeleteAttendance.UseVisualStyleBackColor = true;
            this.btnDeleteAttendance.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.ImageIndex = 2;
            this.button6.ImageList = this.imageList1;
            this.button6.Location = new System.Drawing.Point(264, 234);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(145, 138);
            this.button6.TabIndex = 5;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnImportAttendance
            // 
            this.btnImportAttendance.FlatAppearance.BorderSize = 0;
            this.btnImportAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportAttendance.ImageIndex = 5;
            this.btnImportAttendance.ImageList = this.imageList1;
            this.btnImportAttendance.Location = new System.Drawing.Point(645, 53);
            this.btnImportAttendance.Name = "btnImportAttendance";
            this.btnImportAttendance.Size = new System.Drawing.Size(145, 138);
            this.btnImportAttendance.TabIndex = 6;
            this.btnImportAttendance.UseVisualStyleBackColor = true;
            this.btnImportAttendance.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.ImageIndex = 16;
            this.btnExit.ImageList = this.imageList1;
            this.btnExit.Location = new System.Drawing.Point(644, 234);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(145, 138);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button8_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(247, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "STUDENT ATTENDANCE SYSTEM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(824, 421);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnImportAttendance);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnDeleteAttendance);
            this.Controls.Add(this.btnViewStudents);
            this.Controls.Add(this.btnAddStudent);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnMarkAttendance);
            this.Name = "Form1";
            this.Text = "Attendance System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMarkAttendance;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnViewStudents;
        private System.Windows.Forms.Button btnDeleteAttendance;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnImportAttendance;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
    }
}

