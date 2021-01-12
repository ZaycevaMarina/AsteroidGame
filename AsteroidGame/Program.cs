using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form game_form = new Form();
            game_form.Width = 800;
            game_form.Height = 600;
            Game.Initialize(game_form);
            game_form.Show();
            Game.Draw();
            Application.Run(game_form);

        }
    }
}
