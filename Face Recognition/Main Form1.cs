using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using System.Timers;

using AForge.Video;
using AForge.Vision.Motion;
using AForge.Video.DirectShow;
/*---------------------------------------------------------------------------------------------------------------------------------
 * Name: PrivateEyes Facial Recognition Security Software
 * Written by: InYoface: Steven Garcia, William Chang and Josh Emrick
 * Date: 9/11/2012
 * Revision:1.2.0
 * --------------------------------------------------------------------------------------------------------------------------------
 */
namespace Face_Recognition
{
    public partial class Form1 : Form
    {
        #region variables
        private static bool leftMotionTriggerLocked;
        private static bool rightMotionTriggerLocked;
        private System.Timers.Timer leftMotionTimer;
        private System.Timers.Timer rightMotionTimer;

        private string videoDevice;
        private IVideoSource videoSource;
        private IMotionProcessing processingAlgorithm;
        // motion detector
        private MotionDetector detector;
        // motion alarm level
        private const float motionAlarmLevel = 0.50f;
        private const int arrLen = 32;
        private int zoneTwoXPos = 295;

        Image<Bgr, Byte> currentFrame;
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray_frame = null;

        Capture grabber;

        public HaarCascade Face = new HaarCascade(Application.StartupPath + "/Cascades/haarcascade_frontalface_alt2.xml");//haarcascade_frontalface_alt_tree.xml");

        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);

        int NumLabels;
        
        //Classifier with default training location
        Classifier_Train Eigen_Recog = new Classifier_Train();

        string currentPerson { get; set; }
        static ProviderStatsForm providerStatsForm = new ProviderStatsForm();
        static PatientStatsForm patientStatsForm = new PatientStatsForm();
        static System.Timers.Timer TimerObj = new System.Timers.Timer();
        string tempName;
        int counter = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer splashPage = new System.Timers.Timer();
            splashPage.Interval = 2000;
            splashPage.AutoReset = false;
            splashPage.Elapsed += new ElapsedEventHandler(SplashPageTimer);
            
            image_PICBX.Image = Image.FromFile("C:\\Users\\jemrick\\Downloads\\Face Recognition x64\\Face Recognition\\Resources\\PrivateEye.jpg");
            splashPage.Start(); 
            TimerObj.Interval = 2000;
            TimerObj.AutoReset = false;
            TimerObj.Elapsed += new ElapsedEventHandler(closeDialogDelay);
            //pictureBox1.Image = "";
            
            //Load of previus trained faces and labels for each image

            currentPerson = string.Empty;
            providerStatsForm.Visible = false;
            patientStatsForm.Visible = false;
            this.BringToFront();


            if (Eigen_Recog.IsTrained)
            {
                message_bar.Text = "Learned Data loaded";
            }
            else
            {
                message_bar.Text = "No learning data found, please train program using Learn option";
            }
            initialise_capture();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            image_PICBX.Width = this.Size.Width - 10;
            image_PICBX.Height = this.Size.Height - 10;
            pictureBox1.Location = new Point(this.Size.Width, 0);
            this.image_PICBX.Paint += new PaintEventHandler(image_PICBX_Paint);


            // setup the AForge motion sensor ojects here
            //processingAlgorithm = new MotionAreaHighlighting();
            processingAlgorithm = new GridMotionAreaProcessing(arrLen, arrLen);
            ((GridMotionAreaProcessing)processingAlgorithm).HighlightMotionGrid = true;
            ((GridMotionAreaProcessing)processingAlgorithm).MotionAmountToHighlight = motionAlarmLevel;

            detector = new MotionDetector(
                new TwoFramesDifferenceDetector(),
                processingAlgorithm
            );

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // choose the first device 
            if (videoDevices.Count > 0)
                videoDevice = videoDevices[0].MonikerString;
            else
                videoDevice = null;

            VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoDevice);
            videoSourcePlayer.VideoSource = new AsyncVideoSource(videoCaptureSource);
            videoSourcePlayer.Start();

            videoSource = videoCaptureSource;

            List<Rectangle> rects = new List<Rectangle>();
            rects.Add(new Rectangle(0, 0, 25, 25));
            rects.Add(new Rectangle(zoneTwoXPos, 0, 25, 25));
            
            
            detector.MotionZones = rects.ToArray();

            videoSourcePlayer.Visible = false;

            leftMotionTriggerLocked = false;
            rightMotionTriggerLocked = false;

            leftMotionTimer = new System.Timers.Timer();
            leftMotionTimer.Interval = 2000;
            leftMotionTimer.AutoReset = false;
            leftMotionTimer.Elapsed += new ElapsedEventHandler(LeftMotionTimerHandler);

            rightMotionTimer = new System.Timers.Timer();
            rightMotionTimer.Interval = 2000;
            rightMotionTimer.AutoReset = false;
            rightMotionTimer.Elapsed += new ElapsedEventHandler(RightMotionTimerHandler);
            
        }

        private static void LeftMotionTimerHandler(object sender, ElapsedEventArgs e)
        {
            leftMotionTriggerLocked = false;
        }

        private static void RightMotionTimerHandler(object sender, ElapsedEventArgs e)
        {
            rightMotionTriggerLocked = false;
        }

        void image_PICBX_Paint(object sender, PaintEventArgs e)
        {
            // determine which type of form to show

            // create the form

            // set the position to upper left

            // populate form data
            using (Pen p = new Pen(Color.Black, 3))
            {
                //e.Graphics.DrawEllipse(p, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
                //e.Graphics.DrawString("Welcome",,p, new Point(0, 0));

                
                //e.Graphics.FillRectangle(Brushes.White, 0, 0, 200, 100);
                //e.Graphics.DrawString("This is a test", System.Drawing.SystemFonts.DefaultFont, Brushes.Black, new Point(10, 10));
            }
        }
        //Open training form and pass this
        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop Camera
            stop_capture();

            //OpenForm
            Learner_Form TF = new Learner_Form(this);
            TF.Show();


        }
        public void retrain()
        {

            Eigen_Recog = new Classifier_Train();
            if (Eigen_Recog.IsTrained)
            {
                message_bar.Text = "Training Data loaded";
            }
            else
            {
                message_bar.Text = "No training data found, please train program using Train menu option";
            }
        }

        //Camera Start StopE
        public void initialise_capture()
        {
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
           // if (parrellelToolStripMenuItem.Checked)
           // {
            //    Application.Idle += new EventHandler(FrameGrabber_Parrellel);
           // }
           // else
           // {
                Application.Idle += new EventHandler(FrameGrabber_Standard);
            //}
        }
        private void stop_capture()
        {
            //if (parrellelToolStripMenuItem.Checked)
            //{
              //  Application.Idle -= new EventHandler(FrameGrabber_Parrellel);
            //}
            //else
            //{
                Application.Idle -= new EventHandler(FrameGrabber_Standard);
            //}
            if(grabber!= null)
            {
            grabber.Dispose();
            }
        }
       
        //Process Frame
        void FrameGrabber_Standard(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            if (currentFrame != null)
            {
                gray_frame = currentFrame.Convert<Gray, Byte>();

                //Face Detector
                MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(50, 50));

                if (facesDetected[0].Count() == 0 || facesDetected[0].Count() > 1)
                {
                    HideInfoForms();
                    currentPerson = string.Empty;
                    //TimerObj.Start();
                }
                //Action for each element detected
                foreach (MCvAvgComp face_found in facesDetected[0])
                {
                    result = currentFrame.Copy(face_found.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.Draw(face_found.rect, new Bgr(Color.Blue), 2);

                    if (Eigen_Recog.IsTrained)
                    {
                        string name = Eigen_Recog.Recognise(result);
                        tempName = name;
                        int match_value = (int)Eigen_Recog.Get_Eigen_Distance;

                        //Draw the label for each face detected and recognized
                        currentFrame.Draw(name + " ", ref font, new Point(face_found.rect.X - 2, face_found.rect.Y - 2), new Bgr(Color.LightGreen));
                        ADD_Face_Found(result, name, match_value);

                        // draw regions
                        currentFrame.Draw(detector.MotionZones[0], new Bgr(Color.Red), 2);
                        currentFrame.Draw(detector.MotionZones[1], new Bgr(Color.Red), 2);

                        //Only bring up the info if one person is on the screen
                        if (facesDetected[0].Count() == 1)
                            LoadExtraInfoForm(name);
                    }
                }
                //Show the faces procesed and recognized
                image_PICBX.Image = currentFrame.ToBitmap();
                detector.ProcessFrame(currentFrame.ToBitmap());

                bool done = false;
                float[,] grid = ((GridMotionAreaProcessing)processingAlgorithm).MotionGrid;
                for (int i = 0; i < arrLen && !done; i++)
                {
                    for (int j = 0; j < 16 && !done; j++)
                    {
                        string temp = "";
                        //StreamReader streamReader = new StreamReader(Path.Combine("", "test.txt"));
                        //StreamWriter streamWriter = new StreamWriter(Path.Combine("", "test.txt"));
                        string data = "";
                        if (grid[j, i] > motionAlarmLevel)
                        {
                            if (i < arrLen / 2 && !leftMotionTriggerLocked)
                            {// left side
                               // MessageBox.Show("left side");
                                
                                if (counter > 0)
                                {
                                    counter = counter - 1;
                                    //ReplaceLineByLine(Path.Combine("PatientData", tempName + ".csv"), counter.ToString());
                                    label3.Text = counter.ToString();
                          //          data = streamReader.ReadLine();
                            //        data.Remove(0, data.Length - 1);
                              //      streamWriter.WriteLine(counter.ToString());
                                    done = true;
                                    leftMotionTriggerLocked = true;
                                    leftMotionTimer.Start();
                                }
                            }
                            else if(i >= arrLen / 2&& !rightMotionTriggerLocked)
                            {
                             //   MessageBox.Show("right side");
                                
                                    counter = counter + 1;
                                    label3.Text = counter.ToString();
                                //    data = streamReader.ReadLine();
                                  //  data.Remove(0, data.Length - 1);
                                    //streamWriter.WriteLine(counter.ToString());
                                //    ReplaceLineByLine(Path.Combine("PatientData", tempName + ".csv"), counter.ToString());
                                
                                done = true;
                                
                                rightMotionTriggerLocked = true;
                                rightMotionTimer.Start();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Hides info forms and brings the main window into focus
        /// </summary>
        void HideInfoForms()
        {
            providerStatsForm.Visible = false;
            patientStatsForm.Visible = false;
            this.BringToFront();
        }

        /// <summary>
        /// Loads the relevant form with the person's details, brings the form to the front and makes it visible.
        /// </summary>
        /// <param name="newPerson"></param>
        void LoadExtraInfoForm(string newPerson)
        {
            if (newPerson != currentPerson)
            {


                // Check the PatientData folder for a file matching the name
                if (File.Exists(Path.Combine("PatientData", newPerson + ".csv")))
                {
                    patientStatsForm.LoadData(newPerson);
                    currentPerson = newPerson;
                    patientStatsForm.BringToFront();
                    patientStatsForm.Visible = true;
                    providerStatsForm.Visible = false;
                    //obj.Start();

                }
                // Check the ProviderData folder for a file matching the name\
                else if (File.Exists(Path.Combine("ProviderData", newPerson + ".csv")))
                {
                    providerStatsForm.LoadData(newPerson, counter);
                    currentPerson = newPerson;
                    providerStatsForm.BringToFront();
                    providerStatsForm.Visible = true;
                    patientStatsForm.Visible = false;
                    //obj.Start();

                }
            }
            else
            {
                if (File.Exists(Path.Combine("ProviderData", currentPerson + ".csv")))
                    providerStatsForm.LoadAptData(counter);
            }
        }
        public delegate void EmptyMethodDelegate();

        public static void HideProviderForm()
        {
            if (providerStatsForm.InvokeRequired)
                providerStatsForm.Invoke(new EmptyMethodDelegate(HideProviderForm));
            else
                providerStatsForm.Visible = false;
        }
        public static void HidePatientForm()
        {
            if (patientStatsForm.InvokeRequired)
                patientStatsForm.Invoke(new EmptyMethodDelegate(HidePatientForm));
            else
                patientStatsForm.Visible = false;
        }

        private static void closeDialogDelay(object sender, ElapsedEventArgs e)
        {
            HidePatientForm();
            HideProviderForm();
            TimerObj.Stop();
        }
        private void SplashPageTimer(object sender, ElapsedEventArgs e)
        {
            image_PICBX.Image = Image.FromFile("C:\\Users\\jemrick\\Downloads\\Face Recognition x64\\Face Recognition\\Resources\\PrivateEye.jpg");
     
            TimerObj.Stop();
        }
        void FrameGrabber_Parrellel(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            //Clear_Faces_Found();

            if (currentFrame != null)
            {
                gray_frame = currentFrame.Convert<Gray, Byte>();

                //Face Detector
                MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(50, 50));

                //Action for each element detected
                
                Parallel.ForEach(facesDetected[0], face_found =>
                    {
                        try
                        {
                            
                            result = currentFrame.Copy(face_found.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                            result._EqualizeHist();
                            //draw the face detected in the 0th (gray) channel with blue color
                            currentFrame.Draw(face_found.rect, new Bgr(Color.Blue), 2);

                            if (Eigen_Recog.IsTrained)
                            {
                                string name = Eigen_Recog.Recognise(result);
                                int match_value = (int)Eigen_Recog.Get_Eigen_Distance;

                                //Draw the label for each face detected and recognized
                                currentFrame.Draw(name +  " ", ref font, new Point(face_found.rect.X - 2, face_found.rect.Y - 2), new Bgr(Color.LightGreen));
                                ADD_Face_Found(result, name, match_value);
                            }
                            
                        }
                        catch
                        {
                            //do nothing as parrellel loop buggy
                            //No action as the error is useless, it is simply an error in 
                            //no data being there to process and this occurss sporadically 
                        }
                    });
                //Show the faces procesed and recognized
                image_PICBX.Image = currentFrame.ToBitmap();
                detector.ProcessFrame(currentFrame.ToBitmap());
            }
        }

        //ADD Picture box and label to a panel for each face
        int faces_count = 0;
        int faces_panel_Y = 0;
        int faces_panel_X = 0;

        void Clear_Faces_Found()
        {
           
        }
        void ADD_Face_Found(Image<Gray, Byte> img_found, string name_person, int match_value)
        {
            PictureBox PI = new PictureBox();
            PI.Location = new Point(faces_panel_X, faces_panel_Y);
            PI.Height = 80;
            PI.Width = 80;
            PI.SizeMode = PictureBoxSizeMode.StretchImage;
            PI.Image = img_found.ToBitmap();
            Label LB = new Label();
            LB.Text = name_person + " " + match_value.ToString();
            LB.Location = new Point(faces_panel_X, faces_panel_Y + 80);
            //LB.Width = 80;
            LB.Height = 15;
           
            faces_count++;
            if (faces_count == 2)
            {
                faces_panel_X = 0;
                faces_panel_Y += 100;
                faces_count = 0;
            }
            else faces_panel_X += 85;

            

        }

        //Menu Opeartions
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //Application.Exit();
        }
        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // parrellelToolStripMenuItem.Checked = false;
           // singleToolStripMenuItem.Checked = true;
            Application.Idle -= new EventHandler(FrameGrabber_Parrellel);
            Application.Idle += new EventHandler(FrameGrabber_Standard);
        }
        private void parrellelToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  parrellelToolStripMenuItem.Checked = true;
           // singleToolStripMenuItem.Checked = false;
            Application.Idle -= new EventHandler(FrameGrabber_Standard);
            Application.Idle += new EventHandler(FrameGrabber_Parrellel);
            
        }
     //   private void saveToolStripMenuItem_Click(object sender, EventArgs e)
     //   {
      //      SaveFileDialog SF = new SaveFileDialog();
      //      SF.Filter = "XML File *.xml| *.xml";
       //     if (SF.ShowDialog() == DialogResult.OK)
       //     {
        //        Eigen_Recog.Save_Eigen_Recogniser(SF.FileName);
        //    }
       // }
      //  private void loadToolStripMenuItem_Click(object sender, EventArgs e)
      //  {
      //      OpenFileDialog OF = new OpenFileDialog();
      //      OF.Filter = "XML File *.xml| *.xml";
      //      if (OF.ShowDialog() == DialogResult.OK)
       //     {
        //        Eigen_Recog.Load_Eigen_Recogniser(OF.FileName);
        //    }
        //}

        //Unknow face calibration
        private void Eigne_threshold_txtbxChanged(object sender, EventArgs e)
        {
            try
            {
                Eigen_Recog.Set_Eigen_Threshold = Math.Abs(Convert.ToInt32(0));
                message_bar.Text = "Eigen Threshold Set";
            }
            catch
            {
                message_bar.Text = "Error in Threshold input please use int";
            }
        }

        private void image_PICBX_Click(object sender, EventArgs e)
        {

        }

        private void Faces_Found_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void processingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eigneRecogniserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            


        }

        private void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            lock (this)
            {
                if (detector != null)
                {
                    float motionLevel = detector.ProcessFrame(image);

                    float[,] grid = ((GridMotionAreaProcessing)processingAlgorithm).MotionGrid;
                    for (int i = 0; i < arrLen; i++)
                    {
                        for (int j = 0; j < arrLen; j++)
                        {
                            if (grid[i, j] > motionAlarmLevel)
                            {
                                int test = 0;
                            }
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer_Click(object sender, EventArgs e)
        {

        }
    }
}
