using System;
using System.Drawing;

namespace AsteroidGame
{
    class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;
        public static Random __Rnd;
        private static Image __Image;
        public VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }
        static VisualObject()
        {
            __Rnd = new Random();
            __Image = Image.FromFile("Images/2.jpg");
        }
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(__Image, new Point(_Position.X, _Position.Y));
        }
        /// <summary>
        /// Движение только справа налево.
        /// </summary>
        public virtual void Update()
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
