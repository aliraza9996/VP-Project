using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KSLR_R_FaceRecognitionsSystem
{
    public partial class FaceRecognitionSystem : Form
    {
        List<string> ListEnroll = new List<string>();
        List<string> ListName = new List<string>();
        List<string> ListPath = new List<string>();
        int showdata = 0;
        int i = 0, j = 1;
        //Variables 
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);

        //HaarCascade Library
        HaarCascade faceDetected;

        //For Camera as WebCams 
        Capture camera;

        //Images List if Stored
        Image<Bgr, Byte> Frame;
        Image<Gray, byte> result;
        Image<Rgb, byte> colorResult;
        Image<Gray, byte> TrainedFace = null;
        Image<Rgb, byte> resImage = null;
        Image<Gray, byte> grayFace = null;

        //List 
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();

        List<string> labels = new List<string>();
        List<string> users = new List<string>();

        int Count = 0, NumLables, t = 0;
        string name, names = null;

        public FaceRecognitionSystem()
        {
            InitializeComponent();
            faceDetected = new HaarCascade("haarcascade_frontalface_alt.xml");

            try
            {
                string Labelsinf = File.ReadAllText(Application.StartupPath + "/Faces/Faces.txt");
                string[] Labels = Labelsinf.Split(',');

                NumLables = Convert.ToInt16(Labels[0]);
                Count = NumLables;

                string FacesLoad;

                for (int i = 1; i < NumLables + 1; i++)
                {
                    FacesLoad = "face" + i + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/Faces/" + FacesLoad));
                    labels.Add(Labels[i]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Database Folder is empty..!, please Register Face");
            }

        }

        private void Start_Click(object sender, EventArgs e)
        {

            startScan();
            //txName.Focus();
        }
        public void startScan()
        {
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameProcedure);
            btnStart.Enabled = false;
            btnSave.Enabled = true;
            btnOpen.Enabled = true;
            btnRestart.Enabled = true;
        }
        private void Open_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = Application.StartupPath + "/Faces/",
                UseShellExecute = true,
                Verb = "open"
            });

        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ExitPro_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            saveFace();

        }
        public void saveFace()
        {
            if (txName.Text == "" || txName.Text.Length < 2 || txName.Text == string.Empty)
            {
                MessageBox.Show("Please enter name of person");
            }
            else
            {
                Count += 1;
                grayFace = camera.QueryGrayFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
                MCvAvgComp[][] DetectedFace = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                foreach (MCvAvgComp f in DetectedFace[0])
                {

                    TrainedFace = Frame.Copy(f.rect).Convert<Gray, Byte>();

                    break;
                }

                TrainedFace = result.Resize(100, 100, INTER.CV_INTER_CUBIC);

                trainingImages.Add(TrainedFace);
                IBOutput.Image = TrainedFace;

                labels.Add(txName.Text);

                File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");

                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
                }
                MessageBox.Show("Face Stored.");
                txName.Text = "";
                txName.Focus();

            }
        }
        public void FrameForMark(object sender, EventArgs e)
        {
            Frame = camera.QueryFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
            grayFace = Frame.Convert<Gray, Byte>();

            MCvAvgComp[][] faceDetectedShow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(40, 40));

            foreach (MCvAvgComp f in faceDetectedShow[0])
            {
               
                result = Frame.Copy(f.rect).Convert<Gray, Byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                colorResult = Frame.Copy(f.rect).Convert<Rgb, Byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                Frame.Draw(f.rect, new Bgr(Color.Green), 3);

                if (trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria termCriterias = new MCvTermCriteria(Count, 0.001);
                    EigenObjectRecognizer recognizer =
                        new EigenObjectRecognizer(trainingImages.ToArray(),
                        labels.ToArray(), 3000,
                        ref termCriterias);

                    name = recognizer.Recognize(result);
                    Frame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));
                    lblnameofstudent.Text = name;
                }
            }
            imgBoxMarkAttendance.Image = Frame;
            lblName.Text = names;
            names = "";
        }
        private void FrameProcedure(object sender, EventArgs e)
        {
            users.Add("");
            lblCountAllFaces.Text = "0";

            Frame = camera.QueryFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
            grayFace = Frame.Convert<Gray, Byte>();

            MCvAvgComp[][] faceDetectedShow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            foreach (MCvAvgComp f in faceDetectedShow[0])
            {
                t += 1;

                result = Frame.Copy(f.rect).Convert<Gray, Byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                colorResult = Frame.Copy(f.rect).Convert<Rgb, Byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                Frame.Draw(f.rect, new Bgr(Color.Green), 3);

                if (trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria termCriterias = new MCvTermCriteria(Count, 0.001);
                    EigenObjectRecognizer recognizer =
                        new EigenObjectRecognizer(trainingImages.ToArray(),
                        labels.ToArray(), 3000,
                        ref termCriterias);

                    name = recognizer.Recognize(result);
                    label7.Text = name.ToString();
                    Frame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));

                }
            }
            //users[t - 1] = name;
            users.Add("");
            //Set the number of faces detected on the scene
            lblCountAllFaces.Text = faceDetectedShow[0].Length.ToString();
            users.Add("");
            t = 0;
            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < faceDetectedShow[0].Length; nnn++)
            {
                names = names + users[nnn] + ", ";
            }
            //Show the faces procesed and recognized
            cameraBox.Image = Frame;
            imageBox1.Image = Frame;
            imgBoxMarkAttendance.Image = Frame;
            lblName.Text = names;
            names = "";
            users.Clear();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
           
            pnlAddStudent.Show();
            pnlAddStudent.Location = new Point(0, 0);
            pnlAddStudent.Size = new Size(924, 462);

        }

        private void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            pnlMarkAttendance.Show();
            pnlMarkAttendance.Location = new Point(0, 0);
            pnlMarkAttendance.Size = new Size(924, 462);
        }

        private void pnlViewAttendancew_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void FaceRecognitionSystem_Load(object sender, EventArgs e)
        {

        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            pnlDeleteStudent.Show();
            pnlDeleteStudent.Location = new Point(0, 0);
            pnlDeleteStudent.Size = new Size(924, 462);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pnlDeleteStudent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(txtboxDelStuSearch.Text=="")
            {
                MessageBox.Show("Please enter enrollment");
            }
            else
            {
                Connection connObj = new Connection();
                SqlConnection connectMe = connObj.conn();
                SqlCommand cmd = connectMe.CreateCommand();
                cmd.CommandType = CommandType.Text;
                SqlDataReader Reader = null;
                try
                {

                    cmd.CommandText = "Select * from Students where ID=" + txtboxDelStuSearch.Text;
                    Reader = cmd.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            lblDelStudent.Text = (Reader["ID"].ToString());
                            lblDelStudentNAme.Text = (Reader["Name"].ToString());
                            pbDelStu.Image = Image.FromFile((Reader["Image"]).ToString());
                        }
                        btnDelInPnlDelstu.Enabled = true;
                    }
                    else
                    {
                       MessageBox.Show("No data found");
                    }
                }
                catch (SqlException exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    connectMe.Close();
                }
            }
           
          
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Connection connObj = new Connection();
            SqlConnection connectMe = connObj.conn();
            SqlCommand cmd = connectMe.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Students where ID=" + txtboxDelStuSearch.Text;
            cmd.ExecuteNonQuery();
            connectMe.Close();
            MessageBox.Show("Student Deleted Successfully");
        }

        private void btnViewStudents_Click(object sender, EventArgs e)
        {
            pnlViewAllStudents.Show();
            pnlViewAllStudents.Location = new Point(0, 0);
            pnlViewAllStudents.Size = new Size(924, 462);
            if (i==ListEnroll.Count)
            {
                btnNext.Enabled = false;
                btnBack.Enabled = false;
            }

            set();
        }
        public void set()
        {
            //if (txtboxDelStuSearch.Text == "")
            //{
            //    MessageBox.Show("Please enter enrollment");
            //}
            //else
            //{
            
                Connection connObj = new Connection();
                SqlConnection connectMe = connObj.conn();
                SqlCommand cmd = connectMe.CreateCommand();
                cmd.CommandType = CommandType.Text;
                SqlDataReader Reader = null;
                try
                {
                cmd.CommandText = "Select * from Students";
                    Reader = cmd.ExecuteReader();
                    if (Reader.HasRows)
                    { 
                    while (Reader.Read())
                    {
                        //    enroll1.Text = (Reader["ID"].ToString());
                        //    name1.Text = (Reader["Name"].ToString());
                        //    pictureBox3.Image = Image.FromFile((Reader["Image"]).ToString());
                        //enroll2.Text = (Reader["ID"].ToString());
                        //name2.Text = (Reader["Name"].ToString());
                        //pictureBox4.Image = Image.FromFile((Reader["Image"]).ToString());

                        //enroll3.Text = (Reader["ID"].ToString());
                        //name3.Text = (Reader["Name"].ToString());
                        //pictureBox5.Image = Image.FromFile((Reader["Image"]).ToString());
                        //MessageBox.Show((Reader["ID"].ToString()));
                        ListEnroll.Add((Reader["ID"].ToString()));
                        ListName.Add((Reader["Name"].ToString()));
                        ListPath.Add((Reader["Image"]).ToString());
                    }  
                    btnDelInPnlDelstu.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No data found");
                    }
               
                }
                catch (SqlException exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    connectMe.Close();
                }
            i = 0;
            j = 1;
            showData(i);
           //}
        }
        public void showData(int c)
        {
            i = c;
            //i++;
            while(i<ListEnroll.Count)
            {
           if (i < ListEnroll.Count)
                {
                    enroll1.Text = ListEnroll[i];
                    name1.Text = ListName[i];
                    pictureBox3.Image = Image.FromFile(ListPath[i]);
                }
                i++;
                j++;
                if (i < ListEnroll.Count)
                {
                    enroll2.Text = ListEnroll[i];
                    name2.Text = ListName[i];
                    pictureBox4.Image = Image.FromFile(ListPath[i]);
                }
                i++;
                j++;
                if (i < ListEnroll.Count)
                {
                    enroll3.Text = ListEnroll[i];
                    name3.Text = ListName[i];
                    pictureBox5.Image = Image.FromFile(ListPath[i]);
                }
                if (j%3==0)
                {
                    break;
                }

            }
            
            if (i >= ListEnroll.Count - 1)
            {
                btnNext.Enabled = false;
            }

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            btnBack.Enabled = true;
            enroll1.Text = "";
            name1.Text = "";
            pictureBox3.Image = null;
            enroll2.Text = "";
            name2.Text = "";
            pictureBox4.Image = null;
            enroll3.Text = "";
            name3.Text = "";
            pictureBox5.Image = null;
            i++;
            j++;
            showData(i);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
            if (i<ListEnroll.Count)
            {
                btnBack.Enabled = false;

            }
            i = i - 5;
            j = j - 5;
            showData(i);
        }

        private void name1_Click(object sender, EventArgs e)
        {

        }

        private void name2_Click(object sender, EventArgs e)
        {

        }

        private void pnlViewAllStudents_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlMarkAttendance.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameForMark);
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
                Connection connObj = new Connection();
                SqlConnection connectMe = connObj.conn();
                SqlCommand cmd = connectMe.CreateCommand();
                cmd.CommandType = CommandType.Text;
                SqlDataReader Reader = null;
                string id = "5", name = "5", img="5";
            try
            {
                cmd.CommandText = "Select * from Students where ID="+Convert.ToString( lblnameofstudent.Text) ;
                Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    //MessageBox.Show("Has rows");
                    while (Reader.Read())
                    {
                        //MessageBox.Show("Inside");
                        //MessageBox.Show("HI");
                        id = (Reader["ID"].ToString());
                        name = (Reader["Name"].ToString());
                        img=(Reader["Image"].ToString());
                    }
                }
                //MessageBox.Show(img);
                cmd.Dispose();
                connectMe.Close();
                try
                {
                    string sql2;
                    
                    Connection cnn = new Connection();
                    SqlConnection cnt = cnn.conn();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command;
                    //sql2="INSERT INTO [dbo].[Attendance]([Name], [enroll],[Image]) SELECT ([ID], [Name] FROM   Students WHERE([ID] = 'lblnameofstudent.Text' )";
                    sql2 = "INSERT INTO[dbo].[Attendance] ([Enrollment],[Name],[Image])  VALUES('" + id + "','" + name + "','" + img+      "');";
                    command = new SqlCommand(sql2, cnt);
                    adapter.InsertCommand = new SqlCommand(sql2, cnt);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    MessageBox.Show("Added to Database");
                    cnt.Close();
                }
                catch (Exception v)
                {
                    MessageBox.Show("Already Marked Present");
                    MessageBox.Show(v.Message);
                }
               
                
            }
            catch (MissingPrimaryKeyException e1)
            {
                MessageBox.Show("Primary key is missing");
                MessageBox.Show(e1.Message);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
    }

        private void pnlMarkAttendance_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            pnlDeleteAttendance.Show();
            pnlDeleteAttendance.Location = new Point(0, 0);
            pnlDeleteAttendance.Size = new Size(924, 462);

        }

        private void pnlDeleteAttendance_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDelEnroll_Click(object sender, EventArgs e)
        {

        }

        private void btnViewDelAttendance_Click(object sender, EventArgs e)
        {
            pnlDelView.Show();
            pnlDelView.Location = new Point(379, 29);
            pnlDelView.Size = new Size(498, 294);
            if (textBoxDeleteEnrollment.Text == "")
            {
                MessageBox.Show("Please enter enrollment");
            }
            else
            {
                Connection connObj = new Connection();
                SqlConnection connectMe = connObj.conn();
                SqlCommand cmd = connectMe.CreateCommand();
                cmd.CommandType = CommandType.Text;
                SqlDataReader Reader = null;
                try
                {

                    cmd.CommandText = "Select * from Attendance where Enrollment="+textBoxDeleteEnrollment.Text;

                    Reader = cmd.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            lblDelEnroll.Text = (Reader["ID"].ToString());
                            lblDelName.Text = (Reader["Name"].ToString());
                            pbDelAtt.Image = Image.FromFile((Reader["Image"]).ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found");
                        textBoxDeleteEnrollment.Text = "";
                    }
                }
                catch (SqlException exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    connectMe.Close();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDelAttendance_Click(object sender, EventArgs e)
        {
            Connection connObj = new Connection();
            SqlConnection connectMe = connObj.conn();
            SqlCommand cmd = connectMe.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Attendance where ID=" + textBoxDeleteEnrollment.Text;
            cmd.ExecuteNonQuery();
            connectMe.Close();
            MessageBox.Show("Student Deleted Successfully");
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            pnlViewAttendance.Hide();
        }

        private void pnlViewAttendance_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewAttendance_Click(object sender, EventArgs e)
        {
            pnlViewAttendance.Show();
            pnlViewAttendance.Location = new Point(0, 0);
            pnlViewAttendance.Size = new Size(924, 462);
            Connection connObj = new Connection();
            SqlConnection connectMe = connObj.conn();
            SqlCommand cmd = connectMe.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ID,Enrollment,Name from Attendance";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connectMe.Close();
        }

        private void btnBckPnlMA_Click(object sender, EventArgs e)
        {
            button9.Enabled = true;
            pnlAddStudent.Hide();
        }

        private void btnbcVA_Click(object sender, EventArgs e)
        {
            pnlDeleteAttendance.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pnlViewAllStudents.Hide();
        }

        private void btnhm_Click(object sender, EventArgs e)
        {
            pnlDeleteStudent.Hide();
        }

        private void btnImportAttendance_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            button9.Enabled = false;
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameProcedure);
        }

        private void btnSaveFace_Click(object sender, EventArgs e)
        {

            if (txtBoxEnrollment.Text == "" || txtBoxEnrollment.Text.Length < 2 || txtBoxEnrollment.Text == string.Empty)
            {
                MessageBox.Show("Please enter Enrollment");
            }
            else
            {
                Count += 1;
                grayFace = camera.QueryGrayFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
                MCvAvgComp[][] DetectedFace = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                foreach (MCvAvgComp f in DetectedFace[0])
                {

                    TrainedFace = Frame.Copy(f.rect).Convert<Gray, Byte>();
                    resImage = Frame.Copy(f.rect).Convert<Rgb, Byte>();
                    break;
                }

                TrainedFace = result.Resize(100, 100, INTER.CV_INTER_CUBIC);
                resImage = colorResult.Resize(100, 100, INTER.CV_INTER_CUBIC);

                trainingImages.Add(TrainedFace);
                //IBOutput.Image = TrainedFace;

                labels.Add(txtBoxEnrollment.Text);
                imageBoxFinal.Image= resImage;
                File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");

                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
                }

                MessageBox.Show("Face Stored.");

                txtBoxEnrollment.Focus();
                addToDatabase();
            }
            
        }
        public void addToDatabase()
        {
            try
            {
                Connection connObj = new Connection();
                SqlConnection connectMe = connObj.conn();
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                string imageFolder = (Application.StartupPath + @"\Images\");
                string imageName = txtBoxEnrollment.Text + ".jpg";
                string finalImage = Path.Combine(imageFolder, imageName);          
                resImage.Save(finalImage);
                sql = "INSERT INTO[dbo].[Students] ([ID], [Name],[Image]) VALUES('" + this.txtBoxEnrollment.Text + "','" + this.ttxtBoxName.Text + "','" + finalImage + "');";
                command = new SqlCommand(sql, connectMe);
                adapter.InsertCommand = new SqlCommand(sql, connectMe);
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                connectMe.Close();
                MessageBox.Show("Added to Database");
                ttxtBoxName.Text = "";
                txtBoxEnrollment.Text = "";

            }
            catch (MissingPrimaryKeyException e)
            {
                MessageBox.Show("Primary key is missing");
                MessageBox.Show(e.Message);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
           

        }

    }
}
