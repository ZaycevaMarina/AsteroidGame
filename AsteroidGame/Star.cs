using System.Drawing;

namespace AsteroidGame
{
    class Star : VisualObject
    {
        /// <summary>
        /// Максимальный размер звезды
        /// </summary>
        private static int __StarSize = 5;
        public Star(Point Position, Point Direction, Size Size)
            : base(Position, Direction, Size)
        {
            if (Size.Width > __StarSize || Size.Height > __StarSize)
            {
                _Size.Width %= __StarSize;
                _Size.Height %= __StarSize;
            }
        }
        /// <summary>
        /// Восьмиконечная разноцветная звезда
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.OrangeRed,
                _Position.X - _Size.Width, _Position.Y - _Size.Height,
                _Position.X + _Size.Width, _Position.Y + _Size.Height);
            g.DrawLine(Pens.Red,
                _Position.X + _Size.Width, _Position.Y - _Size.Height,
                _Position.X - _Size.Width, _Position.Y + _Size.Height);

            g.DrawLine(Pens.Gold,
                _Position.X, _Position.Y - _Size.Height,
                _Position.X, _Position.Y + _Size.Height);
            g.DrawLine(Pens.Silver,
                _Position.X - _Size.Width, _Position.Y,
                _Position.X + _Size.Width, _Position.Y);
        }
        /// <summary>
        /// Движение только справа налево.
        /// </summary>
        public override void Update()
        {
            _Position.X += _Direction.X;

            if (_Position.X < 0)
                _Position.X = SplashScreen.Width + _Size.Width;
        }
    }
}
