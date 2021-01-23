using System;
using System.Drawing;

namespace AsteroidGame.Loggers
{
    class GraphicsLogger : Logger
    {
        private BufferedGraphics Buffer;
        private int Width;

        public GraphicsLogger(ref BufferedGraphics buffer, int width)
        {
            Buffer = buffer;
            Width = width;
        }
        public override void Log(string Message)
        {
            Buffer.Graphics.DrawString(Message, SystemFonts.DefaultFont, Brushes.White, Width - Message.Length - 120, 20);
        }

        public override void Flush()
        {
        }
    }
}
