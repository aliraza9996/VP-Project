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
            camera = new Capture();
            camera.QueryFrame();

            Application.Idle += new EventHandler(FrameProcedure);

            btnStart.Enabled = false;

            btnSave.Enabled = true;
            btnOpen.Enabled = true;
            btnRestart.Enabled = true;
            txName.Focus();

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

        private void button9_Click_1(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameProcedure);
        }

        private void btnSaveFace_Click(object sender, EventArgs e)
        {

            if (ttxtBoxName.Text == "" || ttxtBoxName.Text.Length < 2 || ttxtBoxName.Text == string.Empty)
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
                    resImage = Frame.Copy(f.rect).Convert<Rgb, Byte>();
                    break;
                }

                TrainedFace = result.Resize(100, 100, INTER.CV_INTER_CUBIC);
                resImage = colorResult.Resize(100, 100, INTER.CV_INTER_CUBIC);

                trainingImages.Add(TrainedFace);
                //IBOutput.Image = TrainedFace;

                labels.Add(ttxtBoxName.Text);
                imageBoxFinal.Image= resImage;
                File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");

                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
                }

                MessageBox.Show("Face Stored.");
                
                ttxtBoxName.Focus();
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
                //Image i = imageBoxFinal.Image;
                resImage.Save(finalImage);
                //string output = "";
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
