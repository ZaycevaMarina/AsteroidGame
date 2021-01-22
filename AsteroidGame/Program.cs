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
            __GameForm.Text = "Астероиды";
            //Добавление кнопок
            Button btnStartGame = new Button();
            btnStartGame.Text = "Начало игры";
            btnStartGame.Width = btnStartGame.Text.Length * 10;
            btnStartGame.Click += btnStartGameEvent;
            __GameForm.Controls.Add(btnStartGame);

            Button btnRecords = new Button();
            btnRecords.Text = "Рекорды";
            btnRecords.Location = new Point(btnStartGame.Right + 1, 0);
            btnRecords.Width = btnStartGame.Width;
            btnRecords.Click += btnRecordsEvent;
            __GameForm.Controls.Add(btnRecords);

            Button btnStopGame = new Button();
            btnStopGame.Text = "Выход";
            btnStopGame.Location = new Point(btnRecords.Right + 1, 0);
            btnStopGame.Width = btnStartGame.Width;
            btnStopGame.Click += btnStopGameEvent;
            __GameForm.Controls.Add(btnStopGame);

            //Добавление на заставку имени автора
            Label lbl_author = new Label();
            lbl_author.Text = "(c) Зайцева Марина";
            lbl_author.Width = lbl_author.Text.Length * 7;
            lbl_author.Location = new Point(btnStopGame.Right + 10, 0);
            __GameForm.Controls.Add(lbl_author);

            //Обработка нажатия кнопок веерх и вниз для управления космческим кораблём
            __GameForm.KeyPreview = true;//Сначала форма получает события клавиатуры, а затем активный элемент управления получает события клавиатуры
            __GameForm.KeyDown+=Form_KeyDown;
            SetKeyPreview(__GameForm.Controls);//Обработка формой событий клавиатуры

            Application.Run(__GameForm);            
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
            try
            {
                SplashScreen.Initialize(__GameForm);
                __GameForm.Show();
                SplashScreen.Draw();
            }
            catch (ArgumentOutOfRangeException)
            {                
                MessageBox.Show("Недопустимый размера экрана в классе SplashScreen (не более 1000)");
            }
            catch (GameObjectException)
            {
                MessageBox.Show("Неправильные характеристики объектов игры (отрицательные размеры, слишком большая скорость, неверная позиция или при наличии отсутствует изображение)");
            }
        }

        static void btnRecordsEvent(object sender, EventArgs e)
        {//Добавить функционал после развития игры
        }

        static void btnStopGameEvent(object sender, EventArgs e)
        {
            __GameForm.Close();
        }
    }
}
