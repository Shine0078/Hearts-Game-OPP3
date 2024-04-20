using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HeartsGame
{
    public partial class VideoDemoForm : Form
    {
        private Label lblNote;
        private Label label1;
        private LinkLabel linkLabel1;
        private Button btnExitDemo;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;

        public VideoDemoForm()
        {
            InitializeComponent();
        }

        private void VideoDemoForm_Load(object sender, EventArgs e)
        {
            // Load and play the local video file
            axWindowsMediaPlayer1.URL = @"Video Demo\How_to_Play_Hearts.mp4";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoDemoForm));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.lblNote = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnExitDemo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 12);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(854, 480);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(13, 510);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(851, 48);
            this.lblNote.TabIndex = 1;
            this.lblNote.Text = "This video is provided by the YouTube channel \"wikiHow\". Also, please note that d" +
    "epending on your\r\nspecifications in the game setup, some rules may differ.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 576);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "wikiHow\'s YouTube Channel:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.linkLabel1.Location = new System.Drawing.Point(265, 576);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(80, 24);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "wikiHow";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnExitDemo
            // 
            this.btnExitDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitDemo.Location = new System.Drawing.Point(785, 568);
            this.btnExitDemo.Name = "btnExitDemo";
            this.btnExitDemo.Size = new System.Drawing.Size(79, 40);
            this.btnExitDemo.TabIndex = 4;
            this.btnExitDemo.Text = "Close";
            this.btnExitDemo.UseVisualStyleBackColor = true;
            this.btnExitDemo.Click += new System.EventHandler(this.btnExitDemo_Click);
            // 
            // VideoDemoForm
            // 
            this.ClientSize = new System.Drawing.Size(878, 620);
            this.Controls.Add(this.btnExitDemo);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(894, 659);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(894, 659);
            this.Name = "VideoDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Demo of Hearts";
            this.Load += new System.EventHandler(this.VideoDemoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/@wikiHow");
        }

        private void btnExitDemo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}