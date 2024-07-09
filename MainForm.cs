using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.IO;
using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System.Windows.Forms;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;

namespace Image_resizer
{
    public partial class MainForm : Form
    {
        private bool saveAspectRatio = true;
        private bool appendResized = false;
        private bool isFolderResize = false;

        private int targetWidth, targetHeight;

        private string? inputFolder;

        private string imagePath;
        private string resizedImagePath;

        private int inputImageWidth = 512;
        private int inputImageHeight = 512;

        private IImageEncoder? encoder;

        private readonly PathFile pathFile = new(Path.Combine(Environment.CurrentDirectory, "args.txt"));

        public MainForm()
        {
            InitializeComponent();
            targetWidth = (int)numericUpDownWidth.Value;
            targetHeight = (int)numericUpDownHeight.Value;

            if (pathFile.PathsExists)
            {
                linkLabelInputFolder.Text = inputFolder = pathFile.InputPath;
            }
        }

        private void ResizeSingle(int width, int height) => Resize(imagePath, width, height);
        private void ResizeFolder(int width, int height)
        {
            string[] folderPaths = Directory.GetFiles(inputFolder);

            foreach (string current in folderPaths)
            {
                try
                {
                    Resize(current, width, height);
                }
                catch
                {
                    continue;
                }
            }
        }

        new private void Resize(string path, int width, int height)
        {
            using (Image image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(width, height));

                string selected;

                string filename = Path.GetFileNameWithoutExtension(path) + (appendResized ? "_resized" : "");
                string ext = Path.GetExtension(path);
                string dir = Path.GetDirectoryName(path);

                selected = comboBoxFormat.Text.ToLower();
                switch (comboBoxFormat.Text.ToLower())
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "webp":
                        encoder = new WebpEncoder();
                        break;
                    default:
                        selected = ext;
                        encoder = null;
                        break;
                }

                string oldPath = imagePath;

                imagePath = Path.Combine(dir, filename + ext);

                if (encoder == null)
                    image.Save(imagePath);
                else
                {
                    imagePath = Path.Combine(dir, filename + $".{selected}");
                    image.Save(imagePath, encoder);
                }

                if (oldPath != imagePath && File.Exists(oldPath))
                    File.Delete(oldPath);
            };
        }

        private void SaveAspectRatio(ref int targetWidth, ref int targetHeight, int imageWidth, int imageHeight)
        {
            double aspectRatio = (double)imageWidth / imageHeight;
            if (aspectRatio > 1)
                targetHeight = (int)(targetWidth / aspectRatio);
            else
                targetWidth = (int)(targetHeight * aspectRatio);
        }


        private void buttonFile_Click(object sender, EventArgs e)
        {
            labelState.Text = "...";
            labelState.ForeColor = System.Drawing.Color.Black;

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            imagePath = openFileDialog1.FileName;
            using (Image img = Image.Load(imagePath))
            {
                inputImageWidth = img.Width;
                inputImageHeight = img.Height;

                numericUpDownWidth.Value = inputImageWidth;
                numericUpDownHeight.Value = inputImageHeight;
            }

            linkLabelFile.Enabled = true;
            linkLabelFile.Text = imagePath;
            splitContainer1.Panel2.Enabled = true;
        }
        private void buttonResize_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isFolderResize && imagePath != null)
                    ResizeSingle(targetWidth, targetHeight);
                else if (inputFolder != null)
                    ResizeFolder(targetWidth, targetHeight);

                labelState.Text = "Success";
                labelState.ForeColor = System.Drawing.Color.LimeGreen;
            }
            catch (Exception ex)
            {
                labelState.Text = $"Error: {ex.Message}";
                labelState.ForeColor = System.Drawing.Color.Red;
            }            
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            int width = (int)numericUpDownWidth.Value;

            if (saveAspectRatio)
                SaveAspectRatio(ref width, ref targetHeight, inputImageWidth, inputImageHeight);

            targetWidth = width;
            labelOutputRes.Text = $"{targetWidth}x{targetHeight}";
        }
        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            int height = (int)numericUpDownHeight.Value;

            if (saveAspectRatio)
                SaveAspectRatio(ref targetWidth, ref height, inputImageWidth, inputImageHeight);

            targetHeight = height;
            labelOutputRes.Text = $"{targetWidth}x{targetHeight}";
        }

        private void checkBoxSaveaspect_CheckedChanged(object sender, EventArgs e)
        {
            saveAspectRatio = checkBoxSaveaspect.Checked;

            if (saveAspectRatio)
                SaveAspectRatio(ref targetWidth, ref targetHeight, inputImageWidth, inputImageHeight);
            else
            {
                targetWidth = (int)numericUpDownWidth.Value;
                targetHeight = (int)numericUpDownHeight.Value;
            }

            labelOutputRes.Text = $"{targetWidth}x{targetHeight}";
        }

        private void button1920_Click(object sender, EventArgs e)
        {
            numericUpDownWidth.Value = 1920;
            numericUpDownHeight.Value = 1080;
        }
        private void button1280_Click(object sender, EventArgs e)
        {
            numericUpDownWidth.Value = 1280;
            numericUpDownHeight.Value = 720;
        }
        private void button512_Click(object sender, EventArgs e)
        {
            numericUpDownWidth.Value = 512;
            numericUpDownHeight.Value = 512;
        }

        private void linkLabelFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(imagePath))
                Process.Start("explorer.exe", $"/select,\"{imagePath}\"");
        }

        private void checkBoxResizedAppend_CheckedChanged(object sender, EventArgs e) => appendResized = checkBoxResizedAppend.Checked;

        private void radioButtonFile_CheckedChanged(object sender, EventArgs e)
        {
            labelState.Text = "...";
            labelState.ForeColor = System.Drawing.Color.Black;



            splitContainer1.Panel2.Enabled = File.Exists(imagePath);
            buttonFile.Enabled = radioButtonFile.Checked;
            linkLabelFile.Enabled = radioButtonFile.Checked;
            labelOutputRes.Visible = !radioButtonFolder.Checked;
        }
        private void radioButtonFolder_CheckedChanged(object sender, EventArgs e)
        {
            labelState.Text = "...";
            labelState.ForeColor = System.Drawing.Color.Black;

            splitContainer1.Panel2.Enabled = pathFile.PathsExists;

            isFolderResize = radioButtonFolder.Checked;

            buttonInputFolder.Enabled = radioButtonFolder.Checked;
            linkLabelInputFolder.Enabled = radioButtonFolder.Checked;
            labelOutputRes.Visible = !radioButtonFolder.Checked;
        }

        private void buttonInputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogInput.ShowDialog() == DialogResult.OK)
            {
                linkLabelInputFolder.Text = inputFolder = folderBrowserDialogInput.SelectedPath;
                pathFile.SetInput(inputFolder);

                if (inputFolder != null)
                    splitContainer1.Panel2.Enabled = true;
            }
        }

        private void linkLabelInputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Directory.Exists(inputFolder))
                Process.Start("explorer.exe", inputFolder);
        }
    }
}