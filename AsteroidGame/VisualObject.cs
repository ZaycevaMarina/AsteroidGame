using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace AsteroidGame
{
    class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;
        public static Random __Rnd;
        public VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }
        static VisualObject()
        {
            __Rnd = new Random();
        }
        public virtual void Draw(Graphics g)
        {
            //g.DrawEllipse(
            //   Pens.AliceBlue,
            //   _Position.X, _Position.Y,
            //   _Size.Width, _Size.Height);
            string image_name =(_Size.Width % 4 + 1).ToString() + ".jpg";
            Image newImage = Image.FromFile(image_name);
            g.DrawImage(newImage, new Point(_Position.X, _Position.Y));
        }
        /// <summary>
        /// Движение только справа налево.
        /// </summary>
        public virtual void Update()
        {
            _Position.X += _Direction.X;
            if (_Position.X < 0) 
                _Position.X = Game.Width + _Size.Width;
            if (_Position.X > Game.Width + _Size.Width)
                _Position.X = 0;

            _Position.Y += _Direction.Y;
            if (_Position.Y < 0)
                _Position.Y = Game.Height + _Size.Height;

            if (_Position.Y > Game.Height + _Size.Height)
                _Position.Y = 0;
        }
    }
}
