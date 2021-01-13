using System.Drawing;

namespace AsteroidGame
{
    class Comet : VisualObject
    {
        public Comet(Point Position, Point Direction, Size Size)
            : base(Position, Direction, Size)
        {
        }
        /// <summary>
        /// Отрисовка закмкнутой кривой линии по трём случайным точкам
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            int dif = __Rnd.Next() % 5;
            Point point1 = new Point(_Position.X, _Position.Y);
            Point point2 = new Point(_Position.X + dif, _Position.Y + dif);
            Point point3 = new Point(_Position.X - dif, _Position.Y);
            Point[] curvePoints = { point1, point2, point3};
            g.DrawClosedCurve(Pens.Aquamarine, curvePoints);            
        }
    }
}
