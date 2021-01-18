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
        protected static int __ToFastSpead = 100;

        protected VisualObject(Point Position, Point Direction, Size Size)
        {
            if (Position.X < 0 || Position.X > SplashScreen.Width || Position.Y < 0 || Position.Y > SplashScreen.Height//Неверная позиция
                || Size.Width < 0 || Size.Height < 0 //Отричательные размеры
                || Direction.X > __ToFastSpead || Direction.Y > __ToFastSpead)//Слишком большая скорость
                throw new GameObjectException();
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
