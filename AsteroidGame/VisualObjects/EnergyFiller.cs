using System;
using System.Drawing;

namespace AsteroidGame.VisualObjects
{
    class EnergyFiller : VisualObject, ICollision
    {
        public int Power { get; set; } = 5;
        /// <summary>
        /// Максимальный размер звезды
        /// </summary>
        private static int __Size = 10;
        public EnergyFiller(Point Position, Point Direction, Size Size)
            : base(Position, Direction, Size)
        {
            if (Size.Width > __Size || Size.Height > __Size)
            {
                _Size.Width %= __Size;
                _Size.Height %= __Size;
            }
        }
        /// <summary>
        /// Восьмиконечная разноцветная звезда
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.GreenYellow,
                _Position.X - _Size.Width, _Position.Y - _Size.Height,
                _Position.X + _Size.Width, _Position.Y + _Size.Height);
            g.DrawLine(Pens.YellowGreen,
                _Position.X + _Size.Width, _Position.Y - _Size.Height,
                _Position.X - _Size.Width, _Position.Y + _Size.Height);
        }
        /// <summary>
        /// Движение только справа налево.
        /// </summary>
        public override void Update()
        {
            _Position.X += _Direction.X;

            if (_Position.X < 0)
            {
                _Position.X = SplashScreen.Width + _Size.Width;
                _Position.Y = __Rnd.Next(0, SplashScreen.Height - _Size.Height);
            }
        }
        public Rectangle Rect => new Rectangle(_Position, _Size);

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);


        public void Restart()
        {
            _Position.X = 0;
            Enabled = true;
        }
    }
}
