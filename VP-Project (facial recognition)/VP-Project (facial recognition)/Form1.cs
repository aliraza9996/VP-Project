using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VP_Project__facial_recognition_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mark_Attendance markObj = new Mark_Attendance();
            markObj.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Delete_Attendance deleteAttendanceObj = new Delete_Attendance();
            deleteAttendanceObj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_Student addStudentObj = new Add_Student();
            addStudentObj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Delete_Student deleteStudentObj =new Delete_Student();
            deleteStudentObj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            View_Attendance viewAttendanceObj = new View_Attendance();
            viewAttendanceObj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            View_Students viewStudentObj = new View_Students();
            viewStudentObj.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
