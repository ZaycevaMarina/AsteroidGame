using System;
using System.Drawing;

namespace AsteroidGame.VisualObjects
{
    class Bullet : VisualObject, ICollision
    {
        private const int __BulletSizeX = 20;
        private const int __BulletSizeY = 5;
        private const int __BulletSpeed = 15;
        public Bullet(int Position)
            : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX, __BulletSizeY))
        {
        }
        public override void Update()
        {
            if (!Enabled) return;
            if (_Position.X < SplashScreen.Width + __BulletSizeX)
                _Position.X += __BulletSpeed;
            else
                Enabled = false;
        }
        public override void Draw(Graphics g)
        {
            if (!Enabled) return;
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }
        public Rectangle Rect => new Rectangle(_Position, _Size);
        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);


        public void Restart(int Y)
        {
            _Position = new Point(0, Y);
            Enabled = true;
        }
    }
}
