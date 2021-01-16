using System;
using System.Drawing;

namespace AsteroidGame.VisualObjects
{
    class Bullet : VisualObject, ICloneable
    {
        private const int __BulletSizeX = 20;
        private const int __BulletSizeY = 5;
        private const int __BulletSpeed = 3;
        public Bullet(int Position)
            : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX, __BulletSizeY))
        {
        }
        public override void Update()
        {
            if (_Position.X < SplashScreen.Width + __BulletSizeX)
                _Position.X += __BulletSpeed;
            else
                _Position.X = 0;
        }
        public override void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }
        public Rectangle Rect => new Rectangle(_Position, _Size);

        public object Clone()
        {
            return new Bullet(_Position.Y);
        }
    }
}
