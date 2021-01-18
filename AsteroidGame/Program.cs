using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    static class Program
    {
        static Form __GameForm;
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

            Application.Run(__GameForm);            
        }

        static void btnStartGameEvent(object sender, EventArgs e)
        {
            try
            {
                SplashScreen.Initialize(__GameForm);
                __GameForm.Show();
                SplashScreen.Draw();
            }
            catch (ArgumentOutOfRangeException)
            {                
                MessageBox.Show("������������ ������� ������ � ������ SplashScreen (�� ����� 1000)");
            }
            catch (GameObjectException)
            {
                MessageBox.Show("������������ �������������� �������� ���� (������������� �������, ������� ������� ��������, �������� �������)");
            }
        }

        static void btnRecordsEvent(object sender, EventArgs e)
        {//�������� ���������� ����� �������� ����
        }

        static void btnStopGameEvent(object sender, EventArgs e)
        {
            __GameForm.Close();
        }
    }
}
