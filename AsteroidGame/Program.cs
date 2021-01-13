using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form game_form = new Form();
            game_form.Width = 800;
            game_form.Height = 600;
            game_form.Text = "Астероиды";
            //Добавление кнопок
            Button btnStartGame = new Button();
            btnStartGame.Text = "Начало игры";
            btnStartGame.Width = btnStartGame.Text.Length * 10;
            game_form.Controls.Add(btnStartGame);

            Button btnRecords = new Button();
            btnRecords.Text = "Рекорды";
            btnRecords.Location = new Point(btnStartGame.Right + 1, 0);
            btnRecords.Width = btnStartGame.Width;
            game_form.Controls.Add(btnRecords);

            Button btnStopGame = new Button();
            btnStopGame.Text = "Выход";
            btnStopGame.Location = new Point(btnRecords.Right + 1, 0);
            btnStopGame.Width = btnStartGame.Width;
            game_form.Controls.Add(btnStopGame);
            //Добавление на заставку имени автора
            Label lbl_author = new Label();
            lbl_author.Text = "(c) Зайцева Марина";
            lbl_author.Width = lbl_author.Text.Length * 7;
            lbl_author.Location = new Point(btnStopGame.Right + 10, 0);
            game_form.Controls.Add(lbl_author);

            SplashScreen.Initialize(game_form);
            game_form.Show();
            SplashScreen.Draw();
            Application.Run(game_form);            
        }
    }
}
