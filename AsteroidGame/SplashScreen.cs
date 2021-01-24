using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AsteroidGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __VisualObjects;
        private static VisualObjects.Bullet __Bullet;
        private static VisualObjects.SpaceShip __SpaceShip;
        private static VisualObjects.EnergyFiller __EnergyFiller;

        private static int __VisualObjectsCount = 30;
        private static int __VisualObjectsSize = 30;
        private static int __AsteroidObjectsSize = 25;
        private static int __VisualObjectMaxSpeed = 20;

        private static Timer __Timer;
        private static int __TimerInterval = 100;
        private static int __Refresh = -1;
        private static int __RefreshMax = 10;//Количество раз*__TimerInterval в мс отображения действия игры
        private static string __GraphicMessage;
        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Loggers.GraphicsLogger __GraphicsLogger;

        public static void Initialize(Form GameForm)
        {
            if (GameForm.Width > 1000 || GameForm.Height > 1000 || GameForm.Width < 0 || GameForm.Height < 0)
                throw new ArgumentOutOfRangeException();
            Width = GameForm.Width;
            Height = GameForm.Height;
            __Context = BufferedGraphicsManager.Current;
            Graphics g = GameForm.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            __Timer = new Timer { Interval = __TimerInterval };
            __Timer.Tick += OnTimerTick;
            __Timer.Start();

            GameForm.KeyDown += OnGameForm_KeyDown;

            __GraphicsLogger = new Loggers.GraphicsLogger(ref __Buffer, Width);
            Program.__WriteLog += __GraphicsLogger.Log;
        }
        public static void OnGameForm_KeyDown(object Sender, KeyEventArgs E)
        {
            switch (E.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullet = new VisualObjects.Bullet(__SpaceShip.Rect.Y);
                    break;

                case Keys.Up:
                    __SpaceShip.MoveUp();
                    break;

                case Keys.Down:
                    __SpaceShip.MoveDown();
                    break;
            }
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }
        private static void Load()
        {
            try
            {
                __VisualObjects = new VisualObject[__VisualObjectsCount];
                var rnd = new Random();
                int size, i;
                for (i = 0; i < __VisualObjects.Length/2; i++)
                {
                    size = rnd.Next(2, __VisualObjectsSize);
                    //if (size % 2 == 0)
                        __VisualObjects[i] = new VisualObjects.Star(
                                                     new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                     new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                     new Size(size, size));
                    //else
                    //    __VisualObjects[i] = new VisualObjects.Comet(
                    //                                new Point(600, i * 20),
                    //                                new Point(size + i, size + i),
                    //                                new Size(size, size));
                }
                __VisualObjects[__VisualObjects.Length / 2] = new VisualObjects.EnergyFiller(
                                                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                    new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                    new Size(5, 5));
                for (i = __VisualObjects.Length / 2 + 1; i < __VisualObjects.Length; i++)
                    __VisualObjects[i] = new VisualObjects.Asteroid(
                                                           new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                           new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                           new Size(__AsteroidObjectsSize, __AsteroidObjectsSize),
                                                           Image.FromFile("Images/4.jpg"));
                __SpaceShip = new VisualObjects.SpaceShip(
                    new Point(10, 400),
                    new Point(5, 5),
                    new Size(20, 10));
                __SpaceShip.Destroyed += OnShipDestroyed;               
            }
            catch(GameObjectException)
            {
                throw new GameObjectException();
            }
        }
        private static void OnShipDestroyed(object sender, EventArgs e)
        {
            __Timer.Stop();
            var g = __Buffer.Graphics;
            g.Clear(Color.DarkBlue);
            g.DrawString("Game over!!!", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.Red, 200, 100);
            Program.__WriteLog("Корабль сбит. Игра окончена.");
            __Buffer.Render();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            foreach (VisualObject visual_object in __VisualObjects)
                visual_object?.Draw(g);

            __SpaceShip.Draw(g);
            __Bullet?.Draw(g);
            __EnergyFiller?.Draw(g);

            if (__SpaceShip != null)
            {
                g.DrawString("Энерния: " + __SpaceShip.Energy, SystemFonts.DefaultFont, Brushes.White, Width - 100, 0);                
            }
            if(__Refresh > 0)
                g.DrawString(__GraphicMessage, SystemFonts.DefaultFont, Brushes.White, Width - 120 - __GraphicMessage.Length, 20);

            if (!__Timer.Enabled) return;
            __Buffer.Render();
        }
        private static void Update()
        {
            for (var i = 0; i < __VisualObjects.Length; i++)
            {
                var obj = __VisualObjects[i];

                if (obj is ICollision collision_object)
                {
                    //__SpaceShip.CheckCollision(collision_object);
                    if (__SpaceShip.CheckCollision(collision_object))
                    {
                        if (collision_object is VisualObjects.EnergyFiller)
                        {
                            Program.__WriteLog($"Прибаление энергии (+{(__VisualObjects[i] as VisualObjects.EnergyFiller).Power}). Энерния: " + __SpaceShip.Energy);
                            __GraphicMessage = "Прибаление энергии";
                            __Refresh = __RefreshMax;

                            Random rnd = new Random();
                            __VisualObjects[i] = new VisualObjects.EnergyFiller(
                                                        new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                        new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                        new Size(5, 5));
                            continue;
                        }
                        else if (collision_object is VisualObjects.Asteroid)
                        {
                            Program.__WriteLog($"Космический корабль столкнулся с астероидом (-{(__VisualObjects[i] as VisualObjects.Asteroid).Power}). Энерния: {__SpaceShip.Energy}");
                            __GraphicMessage = "Астероид!";
                            __Refresh = __RefreshMax;
                            __VisualObjects[i] = null;
                            continue;
                        }
                    }

                    if (__Bullet?.CheckCollision(collision_object) != true) continue;
                    __SpaceShip.ChangeEnergy((__VisualObjects[i] as VisualObjects.Asteroid).Power);                    

                    Program.__WriteLog($"Пуля сбила астероид (+{(__VisualObjects[i] as VisualObjects.Asteroid).Power}). Энерния: {__SpaceShip.Energy}");
                    __GraphicMessage = "Пуля сбила астероид";
                    __Refresh = __RefreshMax;

                    __Bullet = null;
                    __VisualObjects[i] = null;
                    System.Media.SystemSounds.Beep.Play();
                    __Refresh = __RefreshMax;
                }
            }

            foreach (var game_object in __VisualObjects)
                game_object?.Update();
            __Bullet?.Update();
            __EnergyFiller?.Update();
            if (__Refresh > int.MinValue)
                __Refresh--;
            else
                __Refresh = 0;
        }

        //private static void LogGrathics(string message)
        //{
        //    __Buffer.Graphics.DrawString(message, SystemFonts.DefaultFont, Brushes.White, Width - message.Length - 1, 100);
        //}
    }
}
