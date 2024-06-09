using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ResizerWF
{
    public partial class Form1 : Form
    {
        private bool saveAspectRatio = true;
        private bool appendResized = true;
        private bool isFolderResize = false;
        private bool deleteOriginal = false;
        
        private int targetWidth;
        private int targetHeight;

        private string? inputFolder;
        private string? outputFolder;

        private string imagePath;
        private string resizedImagePath;

        private int inputImageWidth = 512;
        private int inputImageHeight = 512;

        private readonly PathFile pathFile = new PathFile(Path.Combine(Environment.CurrentDirectory, "args.txt"));
        private class PathFile
        {
            private string[] _args = new string[2];
            private string _inputPath;
            private string _outputPath;

            private string _path;
            private bool _pathsExists;
            public PathFile(string path)
            {
                _path = path;
                _pathsExists = TrySetArgs(path);
            }
            private bool TrySetArgs(string path)
            {
                if (File.Exists(path))
                {
                    _args = File.ReadAllLines(path);

                    if (_args.Length == 2)
                        if (!string.IsNullOrEmpty(_args[0]) && !string.IsNullOrEmpty(_args[1]))
                        {
                            _inputPath = _args[0];
                            _outputPath = _args[1];
                            return true;
                        }
                    return false;
                }
                else File.Create(path).Dispose();

                return false;
            }
            private void RewriteFile()
            {
                File.WriteAllLines(_path, [_inputPath, _outputPath]);
            }
            public void SetInput(string path)
            {
                _inputPath = path;
                RewriteFile();
            }
            public void SetOutput(string path)
            {
                _outputPath = path;
                RewriteFile();
            }

            public bool PathsExists => _pathsExists;
            public string InputPath => _inputPath;
            public string OutputPath => _outputPath;
        }

        public Form1()
        {
            InitializeComponent();
            targetWidth = (int)numericUpDownWidth.Value;
            targetHeight = (int)numericUpDownHeight.Value;

            if (pathFile.PathsExists)
            {
                linkLabelInputFolder.Text = inputFolder = pathFile.InputPath;
                linkLabelOutputFolder.Text = outputFolder = pathFile.OutputPath;
            }
        }
        private void ResizeSingleFile(int width, int height)
        {
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (Image inputImage = Image.FromStream(fs))
                {
                    using (Bitmap outputImage = new Bitmap(width, height))
                    {
                        using (Graphics graphics = Graphics.FromImage(outputImage))
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                            graphics.DrawImage(inputImage, 0, 0, width, height);
                        }


                        if (deleteOriginal)
                        {
                            string ext = Path.GetExtension(imagePath);
                            string path = Path.GetDirectoryName(imagePath) + "\\" + Path.GetFileNameWithoutExtension(imagePath) + "_resized" + ext;
                            outputImage.Save(path, ImageFormat.Png);                      
                        }
                        else
                        {
                            using (SaveFileDialog sfd = new SaveFileDialog())
                            {
                                sfd.Title = "Сохранить изображение как...";
                                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";

                                sfd.FileName = Path.GetFileNameWithoutExtension(imagePath) + (appendResized ? "_resized" : "");
                                if (sfd.ShowDialog() == DialogResult.OK)
                                {
                                    outputImage.Save(sfd.FileName, ImageFormat.Png);

                                    linkLabelResized.Enabled = true;
                                    linkLabelResized.Text = resizedImagePath = sfd.FileName;
                                }
                            }
                        }
                    }
                }
            }

            if (deleteOriginal)
            {
                FileSystem.DeleteFile(imagePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                string ext = Path.GetExtension(imagePath);
                string path = Path.GetDirectoryName(imagePath) + "\\" + Path.GetFileNameWithoutExtension(imagePath) + "_resized" + ext;
                string newPath = Path.GetFileName(imagePath);

                FileSystem.RenameFile(path, newPath);

                resizedImagePath = imagePath;

                linkLabelResized.Enabled = true;
                linkLabelResized.Text = resizedImagePath = imagePath;
            }
                

        }
        private void ResizeFolder(int width, int height)
        {
            string[] images = Directory.GetFiles(inputFolder);

            foreach (string imagePath in images)
            {
                try
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (Image inputImage = Image.FromStream(fs))
                    {
                        if (saveAspectRatio) SaveAspectRatio(ref width, ref height, inputImage.Width, inputImage.Height);
                        using (Bitmap outputImage = new Bitmap(width, height))
                        {
                            using (Graphics graphics = Graphics.FromImage(outputImage))
                            {
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                                graphics.DrawImage(inputImage, 0, 0, width, height);
                            }

                            if (deleteOriginal)
                                outputImage.Save(Path.Combine(outputFolder, Path.GetFileName(imagePath)), ImageFormat.Png);
                            else
                                outputImage.Save(Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imagePath) + (appendResized ? "_resized" : "") + ".png"), ImageFormat.Png);
                        }
                    }
                }
                catch
                {
                    continue;
                }

                if (deleteOriginal)
                    FileSystem.DeleteFile(imagePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
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
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            imagePath = openFileDialog1.FileName;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (Image img = Image.FromStream(fs))
                {
                    inputImageWidth = img.Width;
                    inputImageHeight = img.Height;

                    numericUpDownWidth.Value = inputImageWidth;
                    numericUpDownHeight.Value = inputImageHeight;
                }
            }
            linkLabelFile.Enabled = true;
            linkLabelFile.Text = imagePath;
            splitContainer1.Panel2.Enabled = true;
        }
        private void buttonResize_Click(object sender, EventArgs e)
        {
            if (!isFolderResize && imagePath != null)
                ResizeSingleFile(targetWidth, targetHeight);
            else if (inputFolder != null && outputFolder != null)
                ResizeFolder(targetWidth, targetHeight);
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
        private void linkLabelResized_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(resizedImagePath))
                Process.Start("explorer.exe", $"/select,\"{resizedImagePath}\"");
        }
        private void linkLabelFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(imagePath))
                Process.Start("explorer.exe", $"/select,\"{imagePath}\"");
        }

        private void checkBoxResizedAppend_CheckedChanged(object sender, EventArgs e) => appendResized = checkBoxResizedAppend.Checked;

        private void radioButtonFile_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            buttonFile.Enabled = radioButtonFile.Checked;
            linkLabelFile.Enabled = radioButtonFile.Checked;

            linkLabelResized.Visible = true;
        }
        private void radioButtonFolder_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = pathFile.PathsExists;
            linkLabelResized.Visible = false;

            isFolderResize = radioButtonFolder.Checked;

            buttonInputFolder.Enabled = radioButtonFolder.Checked;
            buttonOutputFolder.Enabled = radioButtonFolder.Checked;
            linkLabelOutputFolder.Enabled = radioButtonFolder.Checked;
            linkLabelInputFolder.Enabled = radioButtonFolder.Checked;
        }

        private void buttonInputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogInput.ShowDialog() == DialogResult.OK)
            {
                linkLabelInputFolder.Text = inputFolder = folderBrowserDialogInput.SelectedPath;
                pathFile.SetInput(inputFolder);
                if (outputFolder != null) splitContainer1.Panel2.Enabled = true;
            }
        }
        private void buttonOutputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogOutput.ShowDialog() == DialogResult.OK)
            {
                outputFolder = linkLabelOutputFolder.Text = folderBrowserDialogOutput.SelectedPath;
                pathFile.SetOutput(outputFolder);
                if (inputFolder != null) splitContainer1.Panel2.Enabled = true;
            }
        }

        private void linkLabelInputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Directory.Exists(inputFolder))
                Process.Start("explorer.exe", inputFolder);
        }
        private void linkLabelOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Directory.Exists(outputFolder))
                Process.Start("explorer.exe", outputFolder);
        }

        private void checkboxReplaceOriginal_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxResizedAppend.Enabled = !checkboxReplaceOriginal.Checked;
            deleteOriginal = checkboxReplaceOriginal.Checked;
        }
    }
}
