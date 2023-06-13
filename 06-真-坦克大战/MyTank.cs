using _06_真_坦克大战.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace _06_真_坦克大战
{
    internal class MyTank : Movething
    {
        public bool IsMoving { get; set; }
        public int Hp { get; set; }
        private int originalX;
        private int originalY;

        public MyTank(int x, int y, int speed)
        {
            IsMoving = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.speed = speed;
            bitmapDown = Resources.MyTankDown;
            bitmapUp = Resources.MyTankUp;
            bitmapLeft = Resources.MyTankLeft;
            bitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
            Hp = 4;
        }

        public override void Update()
        {
            MoveCheck();//移动检查
            Move();
            base.Update(); //绘制
        }

        private void MoveCheck()
        {
            #region 检查有没有超出窗体

            if (Dir == Direction.Up)
            {
                if (Y - speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + speed + Height > 450)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + speed + Width > 450)
                {
                    IsMoving = false;
                    return;
                }
            }

            #endregion 检查有没有超出窗体

            //检查有没有和其他元素发生碰撞

            Rectangle rect = GetRectangle();

            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= speed;
                    break;

                case Direction.Down:
                    rect.Y += speed;
                    break;

                case Direction.Left:
                    rect.X -= speed;
                    break;

                case Direction.Right:
                    rect.X += speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoving = false;
                return;
            }
        }

        private void Move()
        {
            if (IsMoving == false) return;

            switch (Dir)
            {
                case Direction.Up:
                    Y -= speed;
                    break;

                case Direction.Down:
                    Y += speed;
                    break;

                case Direction.Left:
                    X -= speed;
                    break;

                case Direction.Right:
                    X += speed;
                    break;
            }
        }

        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;

                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;

                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;

                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;

                case Keys.Space:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            SoundManager.PlayFire();

            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;

                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;

                case Direction.Left:
                    y = y + Height / 2;
                    break;

                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Tag.MyTank, Dir);
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;

                case Keys.S:
                    IsMoving = false;
                    break;

                case Keys.A:
                    IsMoving = false;
                    break;

                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }

        public void TakeDamege()
        {
            Hp--;
            if (Hp <= 0)
            {
                X = originalX;
                Y = originalY;
                Hp = 4;
            }
        }
    }
}