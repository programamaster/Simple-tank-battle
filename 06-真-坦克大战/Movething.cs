using System;
using System.Drawing;

namespace _06_真_坦克大战
{
    internal enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    internal class Movething : GameObject
    {
        private Object _lock = new object();//锁
        public Bitmap bitmapUp { get; set; }
        public Bitmap bitmapDown { get; set; }
        public Bitmap bitmapLeft { get; set; }
        public Bitmap bitmapRight { get; set; }

        public int speed { get; set; }

        private Direction dir;

        public Direction Dir
        {
            get { return dir; }
            set
            {
                dir = value;
                Bitmap bmp = null;
                switch (dir)
                {
                    case Direction.Up:
                        bmp = bitmapUp;
                        break;

                    case Direction.Down:
                        bmp = bitmapDown;
                        break;

                    case Direction.Left:
                        bmp = bitmapLeft;
                        break;

                    case Direction.Right:
                        bmp = bitmapRight;
                        break;
                }
                lock (_lock)
                {
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
            }
        }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.Up:
                    bitmap = bitmapUp;
                    break;

                case Direction.Down:
                    bitmap = bitmapDown;
                    break;

                case Direction.Left:
                    bitmap = bitmapLeft;
                    break;

                case Direction.Right:
                    bitmap = bitmapRight;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }
        }
    }
}