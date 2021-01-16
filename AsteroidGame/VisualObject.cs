using System;
using System.Drawing;

namespace AsteroidGame
{
    internal abstract class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;
        public static Random __Rnd;
        protected VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }
        static VisualObject()
        {
            __Rnd = new Random();
        }
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Движение только справа налево.
        /// </summary>
        public abstract void Update();
    }
}
