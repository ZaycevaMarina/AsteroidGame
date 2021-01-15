using System.Drawing;

namespace AsteroidGame
{
    class Comet : VisualObject
    {
        private static int __Dif;
        public Comet(Point Position, Point Direction, Size Size)
            : base(Position, Direction, Size)
        {
        }

        static Comet()
        {
            __Dif = __Rnd.Next() % 10;
        }
        /// <summary>
        /// Отрисовка закмкнутой кривой линии по трём случайным точкам
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            Point point1 = new Point(_Position.X, _Position.Y);
            Point point2 = new Point(_Position.X + __Dif, _Position.Y + __Dif);
            Point point3 = new Point(_Position.X - __Dif, _Position.Y);
            Point[] curvePoints = { point1, point2, point3};
            g.DrawClosedCurve(Pens.Aquamarine, curvePoints);            
        }
    }
}
