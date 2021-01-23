using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    static class Program
    {
        static Form __GameForm;
        public delegate void Write(string message);
        public static Write __WriteLog;
        private static Loggers.ConsoleLogger __ConsoleLogger = new Loggers.ConsoleLogger();
        private static Loggers.TextFileLogger __TextFileLogger = new Loggers.TextFileLogger("Log.txt");
        private static Loggers.DebugLogger __DebugLogger = new Loggers.DebugLogger();
        private static Loggers.TraceLogger __TraceLogger = new Loggers.TraceLogger();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            __GameForm = new Form();
            __GameForm.Width = 800;
            __GameForm.Height = 600;
            __GameForm.Text = "���������";
            //���������� ������
            Button btnStartGame = new Button();
            btnStartGame.Text = "������ ����";
            btnStartGame.Width = btnStartGame.Text.Length * 10;
            btnStartGame.Click += btnStartGameEvent;
            __GameForm.Controls.Add(btnStartGame);

            Button btnRecords = new Button();
            btnRecords.Text = "�������";
            btnRecords.Location = new Point(btnStartGame.Right + 1, 0);
            btnRecords.Width = btnStartGame.Width;
            btnRecords.Click += btnRecordsEvent;
            __GameForm.Controls.Add(btnRecords);

            Button btnStopGame = new Button();
            btnStopGame.Text = "�����";
            btnStopGame.Location = new Point(btnRecords.Right + 1, 0);
            btnStopGame.Width = btnStartGame.Width;
            btnStopGame.Click += btnStopGameEvent;
            __GameForm.Controls.Add(btnStopGame);

            //���������� �� �������� ����� ������
            Label lbl_author = new Label();
            lbl_author.Text = "(c) ������� ������";
            lbl_author.Width = lbl_author.Text.Length * 7;
            lbl_author.Location = new Point(btnStopGame.Right + 10, 0);
            __GameForm.Controls.Add(lbl_author);

            //��������� ������� ������ ����� � ���� ��� ���������� ���������� �������
            __GameForm.KeyPreview = true;//������� ����� �������� ������� ����������, � ����� �������� ������� ���������� �������� ������� ����������
            __GameForm.KeyDown+=Form_KeyDown;
            SetKeyPreview(__GameForm.Controls);//��������� ������ ������� ����������

            

            Application.Run(__GameForm);
            __TextFileLogger.Dispose();
        }

        private static void SetKeyPreview(Control.ControlCollection cc)
        {
            if(cc !=null)
            {
                foreach(Control control in cc)
                {
                    control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
                    SetKeyPreview(control.Controls);
                }
            }
        }

        private static void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down||e.KeyCode==Keys.ControlKey)
                e.IsInputKey = true;
        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            SplashScreen.OnGameForm_KeyDown(sender, e);
        }


        static void btnStartGameEvent(object sender, EventArgs e)
        {
            __WriteLog = __TextFileLogger.Log;
            //__WriteLog += __ConsoleLogger.Log;
            //__WriteLog += __DebugLogger.Log;
            //__WriteLog += __TraceLogger.Log;
            try
            {
                SplashScreen.Initialize(__GameForm);
                __GameForm.Show();
                SplashScreen.Draw();    

                __WriteLog("���� ��������.");
            }
            catch (ArgumentOutOfRangeException)
            {
                string ms = "������������ ������� ������ � ������ SplashScreen (�� ����� 1000)";
                MessageBox.Show(ms);
                __WriteLog(ms);
            }
            catch (GameObjectException)
            {
                string ms = "������������ �������������� �������� ���� (������������� �������, ������� ������� ��������, �������� ������� ��� ��� ������� ����������� �����������)";
                MessageBox.Show(ms);
                __WriteLog(ms);
            }
        }

        static void btnRecordsEvent(object sender, EventArgs e)
        {//�������� ���������� ����� �������� ����
        }

        static void btnStopGameEvent(object sender, EventArgs e)
        {
            __GameForm.Close();
            __WriteLog("���� ���������.");
        }
    }
}
