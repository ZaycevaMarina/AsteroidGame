using System;
using System.Drawing;

namespace AsteroidGame.VisualObjects
{
    class Asteroid:VisualObject, ICloneable
    {
        public int Power { get; set; }
        private readonly Image Image;
        public Asteroid(Point pos, Point dir, Size size, Image Image) : base(pos, dir, size)
        {
            Power = 1;
            this.Image = Image;
        }
        public override void Draw(Graphics g)
        {
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
        public object Clone()
        {
            Asteroid asteroid = new Asteroid(
                new Point(_Position.X, _Position.Y), 
                new Point(_Direction.X, _Direction.Y), 
                new Size(_Size.Width, _Size.Height), 
                Image);
            asteroid.Power = Power;
            return asteroid;
        }
        public void Restart()
        {
            _Position.Y = SplashScreen.Height;
            _Position.X = SplashScreen.Width;
        }
    }
}
