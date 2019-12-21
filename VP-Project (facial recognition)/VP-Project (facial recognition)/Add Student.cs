using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using System.IO;
using Emgu.CV.Structure;
namespace VP_Project__facial_recognition_
{
    public partial class Add_Student : Form
    {
        MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);
        HaarCascade faceDetected;
        Image<Bgr,Byte> Frame;
        Capture camera;
        //var image = new Image<Bgr, Byte>(new Bitmap(pictureBox1.Image));
        Image<Gray, byte> result;
        Image<Gray, byte> trainedFace = null;
        Image<Gray, byte> grayface = null;
        List<Image<Gray, byte>> trainingImages =new  List<Image<Gray, byte>>();
        List <string> labels =new List<string>();
        List<string> Users= new List<string>();
        int count, NumLabels,t;
        string name, names=null;
        public Add_Student()
        {
            InitializeComponent();
        }

        private void Add_Student_Load(object sender, EventArgs e)
        {
           
            faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                string labelsInf = File.ReadAllText(Application.StartupPath + "/Faces/Faces.txt");
                string[] Labels = labelsInf.Split(',');
                NumLabels = Convert.ToInt32(Labels[0]);
                count = NumLabels;
                string faceload;
                for (int i = 1; i < NumLabels + 1; i++)
                {
                    faceload = "face" + i + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "Faces/Faces.txt"));
                    labels.Add(Labels[i]);



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (camera==null)
            {
                try
                {
                    camera = new Capture();
                }
                catch (NullReferenceException exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            if (camera!=null)
            {
                camera.QueryFrame();
                Application.Idle+=new EventHandler(FrameProcedure);
            }
               
        }

private void FrameProcedure(object sender, EventArgs e)
{
    Frame = camera.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
    grayface = Frame.Convert<Gray, Byte>();
    MCvAvgComp[][] facesDetectedNow = grayface.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
    //frame.Draw(font.rect, new Bgr(Color.Green), 3);
    foreach (MCvAvgComp f in facesDetectedNow[0])
    {
        result=Frame.Copy(f.rect).Convert<Gray,Byte>().Resize(100,100,Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
        Frame.Draw(f.rect, new Bgr(Color.Blue),3);
        if (trainingImages.ToArray().Length!=0)
        {

            Frame.Draw(name,ref font,new Point(f.rect.X-2,f.rect.Y-2), new Bgr(Color.Red));


        }
    }
    imageBox1.Image = Frame;
    
}

private void pictureBox1_Click(object sender, EventArgs e)
{

}

private void button2_Click(object sender, EventArgs e)
{
    Frame.Save("C:\ali.jpg");
    MessageBox.Show("Saved successfully");
}
      
    }
}
