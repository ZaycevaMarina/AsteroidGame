using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace AsteroidGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __VisualObjects;
        private static List<VisualObjects.Bullet> __Bullets = new();
        private static List<VisualObjects.Asteroid> __Asteroids;
        private static VisualObjects.SpaceShip __SpaceShip;

        private static int __VisualObjectsCount = 30;
        public static int __VisualObjectsSize = 30;
        private static int __AsteroidObjectsSize = 25;
        public static int __VisualObjectMaxSpeed = 20;
        private static string __ImageName = "Images/4.jpg";

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
                    var disabled_bullet = __Bullets.FirstOrDefault(b => !b.Enabled);
                    if (disabled_bullet != null)
                        disabled_bullet.Restart(__SpaceShip.Rect.Y);
                    else
                        __Bullets.Add(new VisualObjects.Bullet(__SpaceShip.Rect.Y));
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
                __VisualObjects = new VisualObject[__VisualObjectsCount / 2];
                var rnd = new Random();
                int size, i;
                for (i = 0; i < __VisualObjects.Length - 1; i++)
                {
                    size = rnd.Next(2, __VisualObjectsSize);
                     __VisualObjects[i] = new VisualObjects.Star(
                                                     new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                     new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                     new Size(size, size));
                }
                __VisualObjects[__VisualObjects.Length - 1] = new VisualObjects.EnergyFiller(
                                                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                    new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                    new Size(5, 5));
                GenerateListAsteroids(__VisualObjectsCount / 2);
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
            System.Media.SystemSounds.Exclamation.Play();
            Program.__WriteLog("Корабль сбит. Игра окончена.");
            __Buffer.Render();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            foreach (VisualObject visual_object in __VisualObjects)
                if(visual_object.Enabled) visual_object.Draw(g);

            __SpaceShip.Draw(g);
            foreach(VisualObjects.Bullet b in __Bullets)
                if (b.Enabled) b.Draw(g);
            foreach (VisualObjects.Asteroid asteroid in __Asteroids)
                if (asteroid.Enabled) asteroid.Draw(g);

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

                if (obj.Enabled && obj is ICollision collision_object)
                {
                    if (__SpaceShip.CheckCollision(collision_object))
                    {
                        if (collision_object is VisualObjects.EnergyFiller)
                        {
                            Program.__WriteLog($"Прибаление энергии (+{(__VisualObjects[i] as VisualObjects.EnergyFiller).Power}). Энерния: " + __SpaceShip.Energy);
                            __GraphicMessage = "Прибаление энергии";
                            __VisualObjects[i].Enabled = false;
                            (__VisualObjects[i] as VisualObjects.EnergyFiller).Restart();
                            System.Media.SystemSounds.Beep.Play();
                            __Refresh = __RefreshMax;
                            continue;
                        }
                    }
                    foreach (VisualObjects.Bullet bullet in __Bullets)
                    {
                        if (!bullet.Enabled || !bullet.CheckCollision(collision_object))
                            continue;
                        if (__VisualObjects[i] is VisualObjects.EnergyFiller)
                        {
                            __SpaceShip.ChangeEnergy((__VisualObjects[i] as VisualObjects.EnergyFiller).Power);

                            Program.__WriteLog($"Пуля сбила аптечку (+{(__VisualObjects[i] as VisualObjects.EnergyFiller).Power}). Энерния: {__SpaceShip.Energy}");
                            __GraphicMessage = "Пуля сбила аптечку";
                            bullet.Enabled = false;
                            __VisualObjects[i].Enabled = false;
                            (__VisualObjects[i] as VisualObjects.EnergyFiller).Restart();
                            System.Media.SystemSounds.Beep.Play();
                            __Refresh = __RefreshMax;
                        }
                    }
                }
            }
            for (int i = 0; i < __Asteroids.Count; i++)
            {
                if (__Asteroids[i].Enabled)
                {
                    if (__SpaceShip.CheckCollision(__Asteroids[i] as ICollision))
                    {
                        Program.__WriteLog($"Космический корабль столкнулся с астероидом (-{__Asteroids[i].Power}). Энерния: {__SpaceShip.Energy}");
                        __GraphicMessage = "Астероид!";
                        __Asteroids[i].Enabled = false;
                        if (IsAsteroidEmpty())
                            GenerateListAsteroids(__Asteroids.Count + 1);
                        System.Media.SystemSounds.Asterisk.Play();
                        __Refresh = __RefreshMax;
                        continue;
                    }
                    foreach (VisualObjects.Bullet bullet in __Bullets)
                    {
                        if (!bullet.Enabled || !bullet.CheckCollision(__Asteroids[i] as ICollision))
                            continue;
                        __SpaceShip.ChangeEnergy(__Asteroids[i].Power);

                        Program.__WriteLog($"Пуля сбила астероид (+{__Asteroids[i].Power}). Энерния: {__SpaceShip.Energy}");
                        __GraphicMessage = "Пуля сбила астероид";
                        bullet.Enabled = false;
                        __Asteroids[i].Enabled = false;
                        if (IsAsteroidEmpty())
                            GenerateListAsteroids(__Asteroids.Count + 1);
                        System.Media.SystemSounds.Asterisk.Play();
                        __Refresh = __RefreshMax;
                    }
                }
            }

            foreach (var game_object in __VisualObjects)
                if(game_object.Enabled) game_object.Update();
            foreach (VisualObjects.Bullet bullet in __Bullets)
                if(bullet.Enabled) bullet.Update();
            foreach (VisualObjects.Asteroid asteroid in __Asteroids)
                if (asteroid.Enabled) asteroid.Update();

            if (__Refresh > int.MinValue) __Refresh--;
            else  __Refresh = 0;
        }
        private static void GenerateListAsteroids(int count)
        {
            if (__Asteroids == null)
                __Asteroids = new List<VisualObjects.Asteroid>();
            else
                __Asteroids.Clear();
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
                __Asteroids.Add(new VisualObjects.Asteroid(
                                                       new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                       new Point(-rnd.Next(1, __VisualObjectMaxSpeed), 0),
                                                       new Size(__VisualObjectsSize, __VisualObjectsSize),
                                                       Image.FromFile(__ImageName)));
        }
        private static bool IsAsteroidEmpty()
        {
            foreach (VisualObjects.Asteroid asteroid in __Asteroids)
                if (asteroid.Enabled)
                    return false;
            return true;
        }
    }
}
