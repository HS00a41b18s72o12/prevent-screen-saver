﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace preventScreenSaver
{
    public partial class Form1 : Form
    {
        private static string LABEL_ERROR = "error : can't convert to intType";
        private static string INFO_FILE_PATH = System.Environment.CurrentDirectory + @"\info.txt";
        private static string RUNNING_CHECK_FILE_PATH = System.Environment.CurrentDirectory + @"\running";
        private string BUTTON_TEXT_1 = "1時間";
        private string BUTTON_TEXT_2 = "2時間";
        private string BUTTON_TEXT_3 = "4時間";
        private string BUTTON_TEXT_4 = "8時間";
        private string BUTTON_TEXT_5 = "分";
        private string TEXTBOX_TEXT_1 = "30";
        private int BUTTON_1_LENGTH = 60;
        private int BUTTON_2_LENGTH = 120;
        private int BUTTON_3_LENGTH = 240;
        private int BUTTON_4_LENGTH = 480;

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(INFO_FILE_PATH))
            {
                ArrayList fileInfo = Read_Info(INFO_FILE_PATH);
                for (int i = 0; i < fileInfo.Count; i++)
                {
                    string[] nameValue = fileInfo[i].ToString().Split(",");
                    string name = nameValue[0];
                    int.TryParse(nameValue[1], out int value);
                    switch (i)
                    {
                        case 0:
                            BUTTON_TEXT_5 = name;
                            TEXTBOX_TEXT_1 = nameValue[1];
                            break;
                        case 1:
                            BUTTON_TEXT_1 = name;
                            BUTTON_1_LENGTH = value;
                            break;
                        case 2:
                            BUTTON_TEXT_2 = name;
                            BUTTON_2_LENGTH = value;
                            break;
                        case 3:
                            BUTTON_TEXT_3 = name;
                            BUTTON_3_LENGTH = value;
                            break;
                        case 4:
                            BUTTON_TEXT_4 = name;
                            BUTTON_4_LENGTH = value;
                            break;
                        default:
                            break;
                    }
                }
            }
            this.button1.Text = BUTTON_TEXT_1;
            this.button2.Text = BUTTON_TEXT_2;
            this.button3.Text = BUTTON_TEXT_3;
            this.button4.Text = BUTTON_TEXT_4;
            this.button5.Text = BUTTON_TEXT_5;
            this.label1.Text = "";
            this.textBox1.Text = TEXTBOX_TEXT_1;
        }


        static ArrayList Read_Info(string infoFile)
        {
            ArrayList retInfoList = new ArrayList();
            string _line;
            /**
             * url
             * companyName
             * loginId
             * password
             * delayMinutes
             */
            using (StreamReader infoFile_sr = new StreamReader(infoFile, Encoding.GetEncoding("UTF-8")))
            {
                while ((_line = infoFile_sr.ReadLine()) != null)
                {
                    retInfoList.Add(_line);
                }
            }
            return retInfoList;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = " ";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //if (File.Exists(RUNNING_CHECK_FILE_PATH))
            //{
            //    StreamReader runnningCheckFileInfo = new StreamReader(RUNNING_CHECK_FILE_PATH);
            //    string minutes = runnningCheckFileInfo.ReadToEnd();
            //    runnningCheckFileInfo.Close();
            //    int.TryParse(minutes, out int length);
            //    ScreenTimer(length);
            //}
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                int.TryParse(args[1], out int length);
                ScreenTimer(length);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = BUTTON_1_LENGTH;
            if (File.Exists(RUNNING_CHECK_FILE_PATH))
            {
                File.Delete(RUNNING_CHECK_FILE_PATH);
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
                RestartApp(length);
            }
            else
            {
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
            }

            ScreenTimer(length);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int length = BUTTON_2_LENGTH;
            if (File.Exists(RUNNING_CHECK_FILE_PATH))
            {
                File.Delete(RUNNING_CHECK_FILE_PATH);
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
                RestartApp(length);
            }
            else
            {
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
            }

            ScreenTimer(length);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int length = BUTTON_3_LENGTH;
            if (File.Exists(RUNNING_CHECK_FILE_PATH))
            {
                File.Delete(RUNNING_CHECK_FILE_PATH);
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
                RestartApp(length);
            }
            else
            {
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
            }

            ScreenTimer(length);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int length = BUTTON_4_LENGTH;
            if (File.Exists(RUNNING_CHECK_FILE_PATH))
            {
                File.Delete(RUNNING_CHECK_FILE_PATH);
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
                RestartApp(length);
            }
            else
            {
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
            }

            ScreenTimer(length);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out int length);
            if (length == 0)
            {
                this.label1.Text = LABEL_ERROR;
                Application.DoEvents();
                return;
            }

            if (File.Exists(RUNNING_CHECK_FILE_PATH))
            {
                File.Delete(RUNNING_CHECK_FILE_PATH);
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
                RestartApp(length);
            }
            else
            {
                File.WriteAllText(RUNNING_CHECK_FILE_PATH, length.ToString());
            }

            ScreenTimer(length);
        }

        private void ScreenTimer(int minutes)
        {
            DateTime endTime = DateTime.Now.AddMinutes(minutes);
            this.label1.Text = "0 / " + minutes;
            Application.DoEvents();
            bool cursorVector = false;
            int previousCurrentMinute = 0;
            while (true)
            {
                Thread.Sleep(200);

                // 1分毎にマウスカーソルを動かす
                DateTime now = DateTime.Now;
                int currentMinute = now.Minute;
                if (previousCurrentMinute != currentMinute)
                {
                    var pt = GetCursor.GetCursorPosition();
                    // true false 反転、交互にマウスカーソルを動かす位置を変える
                    if (cursorVector)
                    {
                        cursorVector = false;
                        SetCursor.SetCursorPos(pt.X, pt.Y + 1);
                    }
                    else
                    {
                        cursorVector = true;
                        SetCursor.SetCursorPos(pt.X, pt.Y - 1);
                    }
                }
                previousCurrentMinute = currentMinute;

                string _endTime = endTime.ToString("ddHHmm");
                string _currentTime = DateTime.Now.ToString("ddHHmm");
                if (_endTime == _currentTime)
                {
                    Application.DoEvents();
                    break;
                }
                TimeSpan leftTime = endTime - DateTime.Now;
                int leftTimeInt = (int)Math.Truncate(leftTime.TotalMinutes);
                this.label1.Text = (minutes - leftTimeInt) + " / " + minutes;
                Application.DoEvents();
            }
            this.label1.Text = this.label1.Text + " 終了";
        }


        public static class SetCursor
        {
            [DllImport("user32.dll")]
            public static extern bool SetCursorPos(int X, int Y);
        }

        public static class GetCursor
        {
            #region Win32API
            [DllImport("User32.dll")]
            static extern bool GetCursorPos(out POINT lppoint);
            [StructLayout(LayoutKind.Sequential)]
            struct POINT
            {
                public int X { get; set; }
                public int Y { get; set; }
                public static implicit operator Point(POINT point)
                {
                    return new Point(point.X, point.Y);
                }
            }
            #endregion

            public static Point GetCursorPosition()
            {
                var pt = new POINT();
                GetCursorPos(out pt);
                return pt;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Environment.Exit(0);
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("preventScreenSaver");
            foreach (var item in ps)
            {
                item.Kill();
            }
        }

        private void RestartApp(int minutes)
        {
            System.Diagnostics.Process.Start(@"preventScreenSaver.exe", minutes.ToString());
            // Environment.Exit(0);
            // Application.Restart();
            this.Close();
        }
    }
}
