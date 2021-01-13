using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        private static VisualObject[] __GameObjects;
        private static int __VisualObjectsCount = 30;
        private static int __VisualObjectsSize = 30;
        private static int __TimerInterval = 50;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static void Initialize(Form GameForm)
        {
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
            __GameObjects = new VisualObject[__VisualObjectsCount];
            int size;
            for (var i = 0; i < __GameObjects.Length; i++)
            {
                size = VisualObject.__Rnd.Next() % __VisualObjectsSize;
                switch (size % 3)
                {
                    case 0:
                        __GameObjects[i] = new VisualObject(
                                                    new Point(600, i * 20),
                                                    new Point(size-i, size - i),
                                                    new Size(size, size));

                        break;
                    case 1:
                        __GameObjects[i] = new Star(
                                                    new Point(600, i * 20),
                                                    new Point(-i, 0),
                                                    new Size(size, size));
                        break;
                    case 2:
                        __GameObjects[i] = new Comet(
                                                    new Point(600, i * 20),
                                                    new Point(size + i, size + i),
                                                    new Size(size, size));
                        break;
                }
            }
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            foreach (var game_object in __GameObjects)
                game_object.Draw(g);
            __Buffer.Render();
        }
        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();
        }
    }
}
