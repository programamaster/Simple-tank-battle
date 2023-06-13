using _06_真_坦克大战.Properties;
using System.Drawing;

namespace _06_真_坦克大战
{
    internal enum Tag
    {
        MyTank,
        EnemyTank
    }

    internal class Bullet : Movething
    {
        public Tag tag { get; set; }

        public bool isDestroy { get; set; }

        public Bullet(int x, int y, int speed, Direction dir, Tag tag)
        {
            isDestroy = false;
            this.X = x;
            this.Y = y;
            this.speed = speed;
            bitmapDown = Resources.BulletDown;
            bitmapUp = Resources.BulletUp;
            bitmapLeft = Resources.BulletLeft;
            bitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
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
                if (Y < 0)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y > 450)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X < 0)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X > 450)
                {
                    isDestroy = true;
                    return;
                }
            }

            #endregion 检查有没有超出窗体

            #region 检查有没有和其他元素发生碰撞

            Rectangle rect = GetRectangle();

            rect.X = X + Width / 2 - 3; //其实是子弹的右上角碰到砖块的右上角
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 3;
            rect.Width = 3;

            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;

            NotMovething wall = null;
            if ((wall = GameObjectManager.IsCollidedWall(rect)) != null)
            {
                isDestroy = true;
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundManager.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                isDestroy = true;
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                GameFramework.ChangeToGameOver();
                SoundManager.PlayBlast();
                return;
            }

            if (tag == Tag.MyTank)
            {
                EnemyTank tank = null;
                if ((tank = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    isDestroy = true;
                    GameObjectManager.DestroyTank(tank);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManager.PlayBlast();
                    return;
                }
            }
            else if (tag == Tag.EnemyTank)
            {
                MyTank mytank = null;
                if ((mytank = GameObjectManager.isCollidedMyTank(rect)) != null)
                {
                    isDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    mytank.TakeDamege();
                    SoundManager.PlayHit();
                    return;
                }
            }

            #endregion 检查有没有和其他元素发生碰撞
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
    }
}