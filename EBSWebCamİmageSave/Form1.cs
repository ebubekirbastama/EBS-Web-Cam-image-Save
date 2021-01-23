using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace EBSWebCamİmageSave
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection WEbCamAdedi;
        private VideoCaptureDevice kamera;
        private Bitmap bit;

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png Resmi| *.png |Jpeg Resmi |*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
            saveFileDialog1.Title = "Resmi Kaydet";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 2:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 3:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 4:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
                fs.Close();
            }
        }
        private void kullanilacakcihaz_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
             bit = (Bitmap)eventArgs.Frame.Clone();
             pictureBox1.Image = bit;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WEbCamAdedi = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videocapturedevice in WEbCamAdedi)
            {
                comboBox1.Items.Add(videocapturedevice.Name);
            }
            comboBox1.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            kamera = new VideoCaptureDevice(WEbCamAdedi[comboBox1.SelectedIndex].MonikerString);
            kamera.NewFrame += new NewFrameEventHandler(kullanilacakcihaz_NewFrame);
            kamera.Start();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (kamera.IsRunning) 
            {
                kamera.Stop();
            }
        }
    }
}
