using System.Drawing;

namespace _06_真_坦克大战
{
    internal abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        protected abstract Image GetImage();

        public virtual void DrawSelf()
        {
            //Graphics g = GameFramework.g;

            GameFramework.g.DrawImage(GetImage(), X, Y);
        }

        public virtual void Update()
        {
            DrawSelf();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }
    }
}