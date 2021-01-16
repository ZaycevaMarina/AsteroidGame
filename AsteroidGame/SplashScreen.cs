using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        private static VisualObject[] __VisualObjects;
        private static VisualObjects.Asteroid[] __Asteroids;
        private static VisualObjects.Bullet[] __Bullets;
        private static int __CountVisualObjects = 30;
        private static int __CountAsteroids = 10;
        private static int __CountBullets = 5;
        private static int __VisualObjectsSize = 30;
        private static int __TimerInterval = 100;
        public static int Width { get; set; }
        public static int Height { get; set; }
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
            Timer timer = new Timer { Interval = __TimerInterval };//timer.Interval = 100;
            timer.Tick += OnTimerTick;
            timer.Start();
        }
        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }
        private static void Load()
        {
            __VisualObjects = new VisualObject[__CountVisualObjects];
            __Asteroids = new VisualObjects.Asteroid[__CountAsteroids];
            __Bullets = new VisualObjects.Bullet[__CountBullets];
            int size, rnd, i;
            for (i = 0; i < __VisualObjects.Length; i++)
            {
                size = VisualObject.__Rnd.Next(2, __VisualObjectsSize);

                switch (size % 2)
                {
                    case 0:
                        __VisualObjects[i] = new VisualObjects.Star(
                                                     new Point(600, i * 20),
                                                     new Point(-i, 0),
                                                     new Size(size, size));

                        break;
                    case 1:
                        __VisualObjects[i] = new VisualObjects.Comet(
                                                    new Point(600, i * 20),
                                                    new Point(size + i, size + i),
                                                    new Size(size, size));
                        break;
                }
            }
            for (i = 0; i < __Asteroids.Length; i++)
            {
                size = VisualObject.__Rnd.Next(2, __VisualObjectsSize);
                __Asteroids[i] = new VisualObjects.Asteroid(
                                                    new Point(600, i * 20),
                                                    new Point(-i, 0),
                                                    new Size(size, size),
                                                    Image.FromFile("Images/4.jpg"));
            }
            for (i = 0; i < __Bullets.Length; i++)
            {
                rnd = VisualObject.__Rnd.Next(15, Height);
                __Bullets[i] = new VisualObjects.Bullet(rnd);
            }
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            foreach (VisualObject visual_object in __VisualObjects)
                visual_object.Draw(g);
            foreach (VisualObjects.Asteroid asteroid in __Asteroids)
                asteroid.Draw(g);
            foreach (VisualObjects.Bullet bullet in __Bullets)
                bullet.Draw(g);
            __Buffer.Render();
        }
        private static void Update()
        {
            foreach (VisualObject visual_object in __VisualObjects)
            {
                visual_object.Update();
            }
            foreach (VisualObjects.Asteroid asteroid in __Asteroids)
            {
                foreach (VisualObjects.Bullet bullet in __Bullets)
                {
                    if (asteroid.Collision(bullet))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        asteroid.Restart();
                        bullet.Restart();
                    }
                }
                asteroid.Update();
            }
            foreach (VisualObjects.Bullet bullet in __Bullets)
            {
                bullet.Update();
            }
        }
    }
}
