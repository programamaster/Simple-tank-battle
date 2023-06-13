using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace _06_真_坦克大战
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            windowG = this.CreateGraphics();

            tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;

            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }

        private static void GameMainThread()
        {
            //GameFramework

            GameFramework.Start();

            int sleepTime = 1000 / 60;

            while (true)
            {
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();// 60帧

                windowG.DrawImage(tempBmp, 0, 0);

                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }
    }
}