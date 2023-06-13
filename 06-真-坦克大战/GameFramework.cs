using System.Drawing;

namespace _06_真_坦克大战
{
    internal enum GameState
    {
        Running,
        GameOver
    }

    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;

        public static void Start()
        {
            GameObjectManager.Start();
            GameObjectManager.CreatMap();
            GameObjectManager.CreateMyTank();
            SoundManager.InitSound();
            SoundManager.PlayStart();
        }

        public static void Update()
        {
            if (gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if (gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        private static void GameOverUpdate()
        {
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver, x, y);
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }
    }
}