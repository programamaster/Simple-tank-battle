using System;
using System.Drawing;

namespace _06_真_坦克大战
{
    internal class EnemyTank : Movething
    {
        public int changeDirSpeed { get; set; }
        private int changeDirCount { get; set; }
        public int attackSpeed { get; set; }
        private int attackCount = 0;
        private Random r = new Random();

        public EnemyTank(int x, int y, int speed, Bitmap bmpDown, Bitmap bmpUp, Bitmap bmpLeft, Bitmap bmpRight)
        {
            this.X = x;
            this.Y = y;
            this.speed = speed;
            bitmapDown = bmpDown;
            bitmapUp = bmpUp;
            bitmapLeft = bmpLeft;
            bitmapRight = bmpRight;
            this.Dir = Direction.Down;
            attackSpeed = 60;
            changeDirSpeed = 70;
        }

        public override void Update()
        {
            MoveCheck();//移动检查
            Move();
            AttackCheck();
            AutoChangeDirection();
            base.Update(); //绘制
        }

        private void MoveCheck()
        {
            #region 检查有没有超出窗体

            if (Dir == Direction.Up)
            {
                if (Y - speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + speed + Height > 450)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + speed + Width > 450)
                {
                    ChangeDirection();
                    return;
                }
            }

            #endregion 检查有没有超出窗体

            #region 检查有没有和其他元素发生碰撞

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
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                ChangeDirection();
                return;
            }

            #endregion 检查有没有和其他元素发生碰撞
        }

        private void AutoChangeDirection()
        {
            changeDirCount++;
            if (changeDirCount < changeDirSpeed) return;
            ChangeDirection();
            changeDirCount = 0;
        }

        private void ChangeDirection()
        {
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir == Dir)
                {
                    continue;
                }
                {
                    Dir = dir;
                    break;
                }
            }

            MoveCheck();//再检查一次有没有障碍物
        }

        private void Move()
        {
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

        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < attackSpeed) return;

            Attack();
            attackCount = 0;
        }

        private void Attack()
        {
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
            GameObjectManager.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }
    }
}