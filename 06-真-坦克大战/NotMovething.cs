using System.Drawing;

namespace _06_真_坦克大战
{
    /// <summary>
    /// 不可移动物体
    /// </summary>
    internal class NotMovething : GameObject
    {
        private Image img;

        public Image image
        {
            get { return img; }
            set
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        protected override Image GetImage()
        {
            return image;
        }

        public NotMovething(int x, int y, Image img)
        {
            this.X = x;
            this.Y = y;
            this.image = img;
        }
    }
}