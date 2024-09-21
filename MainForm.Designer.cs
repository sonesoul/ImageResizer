using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_resizer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonFile = new Button();
            saveFileDialog1 = new SaveFileDialog();
            xlabel = new Label();
            numericUpDownHeight = new NumericUpDown();
            numericUpDownWidth = new NumericUpDown();
            folderBrowserDialogInput = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            checkBoxSaveaspect = new CheckBox();
            splitContainer1 = new SplitContainer();
            linkLabelInputFolder = new LinkLabel();
            buttonInputFolder = new Button();
            radioButtonFolder = new RadioButton();
            radioButtonFile = new RadioButton();
            linkLabelFile = new LinkLabel();
            labelState = new Label();
            labelFormat = new Label();
            comboBoxFormat = new ComboBox();
            checkBoxResizedAppend = new CheckBox();
            button512 = new Button();
            button1280 = new Button();
            button1920 = new Button();
            labelOutputRes = new Label();
            buttonResize = new Button();
            folderBrowserDialogOutput = new FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonFile
            // 
            buttonFile.Location = new Point(20, 36);
            buttonFile.Name = "buttonFile";
            buttonFile.Size = new Size(83, 23);
            buttonFile.TabIndex = 0;
            buttonFile.Text = "Select file";
            buttonFile.TextAlign = ContentAlignment.MiddleLeft;
            buttonFile.UseVisualStyleBackColor = true;
            buttonFile.Click += buttonFile_Click;
            // 
            // xlabel
            // 
            xlabel.FlatStyle = FlatStyle.System;
            xlabel.Font = new Font("Segoe UI", 13F);
            xlabel.Location = new Point(63, 41);
            xlabel.Name = "xlabel";
            xlabel.Size = new Size(10, 22);
            xlabel.TabIndex = 1;
            xlabel.Text = "x";
            xlabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Location = new Point(79, 40);
            numericUpDownHeight.Maximum = new decimal(new int[] { 1080, 0, 0, 0 });
            numericUpDownHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new Size(54, 23);
            numericUpDownHeight.TabIndex = 2;
            numericUpDownHeight.Value = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownHeight.ValueChanged += numericUpDownHeight_ValueChanged;
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Location = new Point(3, 40);
            numericUpDownWidth.Maximum = new decimal(new int[] { 1920, 0, 0, 0 });
            numericUpDownWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new Size(54, 23);
            numericUpDownWidth.TabIndex = 3;
            numericUpDownWidth.TextAlign = HorizontalAlignment.Right;
            numericUpDownWidth.UpDownAlign = LeftRightAlignment.Left;
            numericUpDownWidth.Value = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownWidth.ValueChanged += numericUpDownWidth_ValueChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxSaveaspect
            // 
            checkBoxSaveaspect.AutoSize = true;
            checkBoxSaveaspect.Checked = true;
            checkBoxSaveaspect.CheckState = CheckState.Checked;
            checkBoxSaveaspect.Location = new Point(3, 69);
            checkBoxSaveaspect.Name = "checkBoxSaveaspect";
            checkBoxSaveaspect.Size = new Size(114, 19);
            checkBoxSaveaspect.TabIndex = 4;
            checkBoxSaveaspect.Text = "Save aspect ratio";
            checkBoxSaveaspect.UseVisualStyleBackColor = true;
            checkBoxSaveaspect.CheckedChanged += checkBoxSaveaspect_CheckedChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(linkLabelInputFolder);
            splitContainer1.Panel1.Controls.Add(buttonInputFolder);
            splitContainer1.Panel1.Controls.Add(radioButtonFolder);
            splitContainer1.Panel1.Controls.Add(radioButtonFile);
            splitContainer1.Panel1.Controls.Add(linkLabelFile);
            splitContainer1.Panel1.Controls.Add(buttonFile);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(labelState);
            splitContainer1.Panel2.Controls.Add(labelFormat);
            splitContainer1.Panel2.Controls.Add(comboBoxFormat);
            splitContainer1.Panel2.Controls.Add(checkBoxResizedAppend);
            splitContainer1.Panel2.Controls.Add(button512);
            splitContainer1.Panel2.Controls.Add(button1280);
            splitContainer1.Panel2.Controls.Add(button1920);
            splitContainer1.Panel2.Controls.Add(labelOutputRes);
            splitContainer1.Panel2.Controls.Add(buttonResize);
            splitContainer1.Panel2.Controls.Add(xlabel);
            splitContainer1.Panel2.Controls.Add(numericUpDownWidth);
            splitContainer1.Panel2.Controls.Add(numericUpDownHeight);
            splitContainer1.Panel2.Controls.Add(checkBoxSaveaspect);
            splitContainer1.Panel2.Enabled = false;
            splitContainer1.Size = new Size(362, 209);
            splitContainer1.SplitterDistance = 121;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 7;
            // 
            // linkLabelInputFolder
            // 
            linkLabelInputFolder.AutoSize = true;
            linkLabelInputFolder.Enabled = false;
            linkLabelInputFolder.Location = new Point(20, 145);
            linkLabelInputFolder.Name = "linkLabelInputFolder";
            linkLabelInputFolder.RightToLeft = RightToLeft.No;
            linkLabelInputFolder.Size = new Size(69, 15);
            linkLabelInputFolder.TabIndex = 20;
            linkLabelInputFolder.TabStop = true;
            linkLabelInputFolder.Text = "Input folder";
            linkLabelInputFolder.LinkClicked += linkLabelInputFolder_LinkClicked;
            // 
            // buttonInputFolder
            // 
            buttonInputFolder.Enabled = false;
            buttonInputFolder.Location = new Point(20, 119);
            buttonInputFolder.Name = "buttonInputFolder";
            buttonInputFolder.Size = new Size(83, 23);
            buttonInputFolder.TabIndex = 18;
            buttonInputFolder.Text = "Input";
            buttonInputFolder.TextAlign = ContentAlignment.MiddleLeft;
            buttonInputFolder.UseVisualStyleBackColor = true;
            buttonInputFolder.Click += buttonInputFolder_Click;
            // 
            // radioButtonFolder
            // 
            radioButtonFolder.AutoSize = true;
            radioButtonFolder.Location = new Point(11, 94);
            radioButtonFolder.Name = "radioButtonFolder";
            radioButtonFolder.Size = new Size(58, 19);
            radioButtonFolder.TabIndex = 17;
            radioButtonFolder.Text = "Folder";
            radioButtonFolder.UseVisualStyleBackColor = true;
            radioButtonFolder.CheckedChanged += radioButtonFolder_CheckedChanged;
            // 
            // radioButtonFile
            // 
            radioButtonFile.AutoSize = true;
            radioButtonFile.Checked = true;
            radioButtonFile.Location = new Point(11, 11);
            radioButtonFile.Name = "radioButtonFile";
            radioButtonFile.Size = new Size(43, 19);
            radioButtonFile.TabIndex = 16;
            radioButtonFile.TabStop = true;
            radioButtonFile.Text = "File";
            radioButtonFile.UseVisualStyleBackColor = true;
            radioButtonFile.CheckedChanged += radioButtonFile_CheckedChanged;
            // 
            // linkLabelFile
            // 
            linkLabelFile.AutoSize = true;
            linkLabelFile.Enabled = false;
            linkLabelFile.Location = new Point(20, 62);
            linkLabelFile.Name = "linkLabelFile";
            linkLabelFile.RightToLeft = RightToLeft.No;
            linkLabelFile.Size = new Size(70, 15);
            linkLabelFile.TabIndex = 15;
            linkLabelFile.TabStop = true;
            linkLabelFile.Text = "Selected file";
            linkLabelFile.LinkClicked += linkLabelFile_LinkClicked;
            // 
            // labelState
            // 
            labelState.AutoSize = true;
            labelState.Location = new Point(74, 153);
            labelState.Name = "labelState";
            labelState.Size = new Size(16, 15);
            labelState.TabIndex = 18;
            labelState.Text = "...";
            // 
            // labelFormat
            // 
            labelFormat.AutoSize = true;
            labelFormat.Enabled = false;
            labelFormat.Location = new Point(63, 123);
            labelFormat.Name = "labelFormat";
            labelFormat.Size = new Size(76, 15);
            labelFormat.TabIndex = 17;
            labelFormat.Text = "(new format)";
            // 
            // comboBoxFormat
            // 
            comboBoxFormat.ImeMode = ImeMode.On;
            comboBoxFormat.Items.AddRange(new object[] { "Png", "Jpg", "WebP" });
            comboBoxFormat.Location = new Point(3, 120);
            comboBoxFormat.Name = "comboBoxFormat";
            comboBoxFormat.Size = new Size(54, 23);
            comboBoxFormat.TabIndex = 16;
            // 
            // checkBoxResizedAppend
            // 
            checkBoxResizedAppend.AutoSize = true;
            checkBoxResizedAppend.Location = new Point(3, 94);
            checkBoxResizedAppend.Name = "checkBoxResizedAppend";
            checkBoxResizedAppend.Size = new Size(102, 19);
            checkBoxResizedAppend.TabIndex = 15;
            checkBoxResizedAppend.Text = "Add \"_resized\"";
            checkBoxResizedAppend.UseVisualStyleBackColor = true;
            checkBoxResizedAppend.CheckedChanged += checkBoxResizedAppend_CheckedChanged;
            // 
            // button512
            // 
            button512.Location = new Point(155, 3);
            button512.Name = "button512";
            button512.Size = new Size(70, 23);
            button512.TabIndex = 13;
            button512.Text = "512x512";
            button512.UseVisualStyleBackColor = true;
            button512.Click += button512_Click;
            // 
            // button1280
            // 
            button1280.Location = new Point(79, 3);
            button1280.Name = "button1280";
            button1280.Size = new Size(70, 23);
            button1280.TabIndex = 12;
            button1280.Text = "1280x720";
            button1280.UseVisualStyleBackColor = true;
            button1280.Click += button1280_Click;
            // 
            // button1920
            // 
            button1920.Location = new Point(3, 3);
            button1920.Name = "button1920";
            button1920.Size = new Size(70, 23);
            button1920.TabIndex = 11;
            button1920.Text = "1920x1080";
            button1920.UseVisualStyleBackColor = true;
            button1920.Click += button1920_Click;
            // 
            // labelOutputRes
            // 
            labelOutputRes.AutoSize = true;
            labelOutputRes.Enabled = false;
            labelOutputRes.Location = new Point(139, 42);
            labelOutputRes.Name = "labelOutputRes";
            labelOutputRes.Size = new Size(57, 15);
            labelOutputRes.TabIndex = 10;
            labelOutputRes.Text = "(512x512)";
            // 
            // buttonResize
            // 
            buttonResize.Location = new Point(3, 149);
            buttonResize.Name = "buttonResize";
            buttonResize.Size = new Size(65, 23);
            buttonResize.TabIndex = 1;
            buttonResize.Text = "Resize!";
            buttonResize.TextAlign = ContentAlignment.MiddleLeft;
            buttonResize.UseVisualStyleBackColor = true;
            buttonResize.Click += buttonResize_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(362, 209);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Image resizer";
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonFile;
        private SaveFileDialog saveFileDialog1;
        private Label xlabel;
        private NumericUpDown numericUpDownHeight;
        private NumericUpDown numericUpDownWidth;
        private FolderBrowserDialog folderBrowserDialogInput;
        private OpenFileDialog openFileDialog1;
        private CheckBox checkBoxSaveaspect;
        private SplitContainer splitContainer1;
        private FolderBrowserDialog folderBrowserDialogOutput;
        private Button buttonResize;
        private Label labelOutputRes;
        private Button button1920;
        private Button button512;
        private Button button1280;
        private LinkLabel linkLabelFile;
        private CheckBox checkBoxResizedAppend;
        private Button buttonInputFolder;
        private RadioButton radioButtonFolder;
        private RadioButton radioButtonFile;
        private LinkLabel linkLabelInputFolder;
        private ComboBox comboBoxFormat;
        private Label labelFormat;
        private Label labelState;
    }
}