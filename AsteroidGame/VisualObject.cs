using System;
using System.Drawing;

namespace AsteroidGame
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
    internal abstract class VisualObject : ICollision
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

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(_Position, _Size);

    }
}
