using BaseUtils.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseUtils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptchaDecodeHelper decode = new CaptchaDecodeHelper();
            pictureBox1.Image.Save(System.Environment.CurrentDirectory + "\\Image\\" + "encodeCaptcha.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            label1.Text = decode.DecodeCaptchaInt(System.Environment.CurrentDirectory + "\\Image\\" + "encodeCaptcha.jpg", 110, 50);
            label2.Text = decode.DecodeCaptchaChar(System.Environment.CurrentDirectory + "\\Image\\" + "encodeCaptcha.jpg", 110, 50);
            label3.Text = decode.DecodeCaptchaIntChar(System.Environment.CurrentDirectory + "\\Image\\" + "encodeCaptcha.jpg", 110, 50);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaptchaEncodeHelper encode = new CaptchaEncodeHelper();
            MemoryStream ms = encode.MakeCaptchaImage(System.Environment.CurrentDirectory + "\\Image\\" + "captcha.jpg", 110, 50, RandomHelper.GetRandomNumber(4));
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CaptchaEncodeHelper encode = new CaptchaEncodeHelper();
            MemoryStream ms = encode.MakeCaptchaImage(System.Environment.CurrentDirectory + "\\Image\\" + "captcha.jpg", 110, 50, RandomHelper.GetRandomPureChar(4));
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CaptchaEncodeHelper encode = new CaptchaEncodeHelper();
            MemoryStream ms = encode.MakeCaptchaImage(System.Environment.CurrentDirectory + "\\Image\\" + "captcha.jpg", 110, 50, RandomHelper.GetRandomNumberString(4, false, ""));
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                config.DatabaseFile = f.FileName;
                label4.Text = config.DataSource;
                SQLiteHelper helper = new SQLiteHelper(label4.Text);
                if (helper.TestConnection(label4.Text))
                {
                    label5.Text = "OK";
                }
                else
                {
                    label5.Text = "X";
                }
            }
        }
    }
}