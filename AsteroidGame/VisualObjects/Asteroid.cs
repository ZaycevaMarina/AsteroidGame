using System;
using System.Drawing;

namespace AsteroidGame.VisualObjects
{
    class Asteroid:VisualObject
    {
        public int Power { get; set; }
        private Image Image;
        public Asteroid(Point pos, Point dir, Size size, Image Image) : base(pos, dir, size)
        {
            Power = 1;
            this.Image = Image;
        }
        public override void Draw(Graphics g)
        {
            //g.FillEllipse(Brushes.White, _Position.X, _Position.Y, Size.Width, Size.Height);
            g.DrawImage(Image, new Point(_Position.X, _Position.Y));
        }
        public override void Update()
        {
            _Position.X += _Direction.X;
            if (_Position.X < 0)
                _Position.X = SplashScreen.Width + _Size.Width;
            if (_Position.X > SplashScreen.Width + _Size.Width)
                _Position.X = 0;

            _Position.Y += _Direction.Y;
            if (_Position.Y < 0)
                _Position.Y = SplashScreen.Height + _Size.Height;

            if (_Position.Y > SplashScreen.Height + _Size.Height)
                _Position.Y = 0;
        }
    }
}
