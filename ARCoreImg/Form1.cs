using System.Diagnostics;
using System.Text;

namespace ARCoreImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "PNG(*.png)|*.png|JPG(*.jpg)|*.jpg|All files(*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Process process = new Process();
                process.StartInfo.FileName = "arcoreimg.exe";
                process.StartInfo.Arguments
                    = $"eval-img --input_image_path={openFileDialog.FileName}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                string? output;
                StringBuilder sb = new StringBuilder();
                while((output = process.StandardOutput.ReadLine()) != null)
                {
                    sb.Append(output);
                    sb.Append(Environment.NewLine);
                }

                while ((output = process.StandardError.ReadLine()) != null)
                {
                    sb.Append(output);
                    sb.Append(Environment.NewLine);
                }

                MessageBox.Show(sb.ToString(), "측정결과(점수 또는 오류)");

                process.WaitForExit();
                

            }


        }
    }
}