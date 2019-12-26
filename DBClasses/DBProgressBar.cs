using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using Yadi;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Net;

namespace OM
{
    public class DBProgressBar
    {
        public OMControls.OMProgressBar PB = new OMControls.OMProgressBar();
        public Label lbl = new Label();
        public OMControls.OMLabel pln = new OMControls.OMLabel();
        public Timer TM = new Timer();
        public Timer TM2 = new Timer();
        public Form FM = new Form();
        public Control Ctrl = new Control();
        bool TimerFlag = false;
        public DBProgressBar(Form FM)
        {
            //progressBar1
            PB.Value = 0;
            PB.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);//==umesh
            PB.EndColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            PB.ForeColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            PB.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            PB.Location = new System.Drawing.Point(3, 10);
            PB.Name = "progressBar1";
            PB.Size = new System.Drawing.Size(486, 19);
            PB.StartColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);//==umesh
            PB.Step = 5;
            PB.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            PB.TabIndex = 0;
            //
            // lblStatus
            // 
            lbl.AutoSize = false;
            lbl.BackColor = System.Drawing.Color.Transparent;
            lbl.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl.Location = new System.Drawing.Point(10, 30);
           // lbl.Name = "lblStatus";
            lbl.Size = new System.Drawing.Size(100, 20);
            lbl.TabIndex = 2;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlStatus
            // 
            pln.Visible = true;
            pln.BackColor = System.Drawing.Color.Transparent;
            //pln.BorderRadius = 3;
            pln.Controls.Add(PB);
            pln.Controls.Add(lbl);
            pln.CornerRadius = 3;
            pln.GradientBottom = Color.FromArgb(213, 225, 230);
           // pln.GradientTop = Color.FromArgb(78, 116, 133);
            pln.GradientTop = Color.FromArgb(255, 255, 255);//==umesh
            pln.GradientMiddle = System.Drawing.Color.White;
            pln.GradientShow = true;
            pln.BorderStyle = BorderStyle.None;
            pln.Size = new System.Drawing.Size(505, 49);
            pln.Location = new Point(FM.ClientSize.Width / 2 - pln.Size.Width / 2,
            FM.ClientSize.Height / 2 - pln.Size.Height / 2);
            pln.Anchor = AnchorStyles.None;
            pln.Name = "pnlStatus";
            pln.TabIndex = 104;
            FM.Controls.Add(pln);
            pln.BringToFront();
            // 
            // timer1
            // 
            TM.Interval = 500;
            TM.Tick += new System.EventHandler(this.timer1_Tick);
            //TM2.Interval = 500;
            //TM2.Tick += new System.EventHandler(this.timer2_Tick);
            this.FM = FM;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if (PB.Value < 100)
            {
                PB.Value = PB.Value + 10;
                if (PB.Value == 0 || PB.Value == 30 || PB.Value == 60 || PB.Value == 90)
                    lbl.Text = "Processing .";
                else if (PB.Value == 10 || PB.Value == 40 || PB.Value == 70 || PB.Value == 100)
                    lbl.Text = "Processing ..";
                else if (PB.Value == 20 || PB.Value == 50 || PB.Value == 80)
                    lbl.Text = "Processing ...";
            }
            else
            {
                PB.Value = 0;
                TM.Interval = 500;
                lbl.Text = "";
                TM.Enabled = false;
                pln.Visible = false;
                FM.Cursor = Cursors.Default;
                Ctrl.Visible = true;
              //  FM.Enabled = true;

            }
        }

        public void TimerStart()
        {
            lbl.Text = "Waiting....";
            Application.DoEvents();
            FM.Cursor = Cursors.WaitCursor;
            TM.Enabled = true;
        }

        public void RunTimer()
        {
            while (TimerFlag)
            {
                DisplayProgress();
                System.Threading.Thread.Sleep(500);
            }

            PB.Value = 0;
            lbl.Text = "";
            pln.Visible = false;
            FM.Cursor = Cursors.Default;
            Application.DoEvents();
            Ctrl.Visible = true;
        }

        public void DisplayProgress()
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            if (PB.Value < 100)
            {
                PB.Value = PB.Value + 10;
                if (PB.Value == 0 || PB.Value == 30 || PB.Value == 60 || PB.Value == 90)
                    lbl.Text = "Processing .";
                else if (PB.Value == 10 || PB.Value == 40 || PB.Value == 70 || PB.Value == 100)
                    lbl.Text = "Processing ..";
                else if (PB.Value == 20 || PB.Value == 50 || PB.Value == 80)
                    lbl.Text = "Processing ...";
            }
            else
                PB.Value = 0;

            Application.DoEvents();
        }

        public void TimerStart2()
        {
            // FM.Enabled = false;
            //FM.Cursor = Cursors.WaitCursor;
            TimerFlag = true;
            //RunTimer();
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(RunTimer));
            th.Start();
            //TM2.Enabled = true;
        }
        public void TimerStop2()
        {
            TimerFlag = false;
        }
    }
}
