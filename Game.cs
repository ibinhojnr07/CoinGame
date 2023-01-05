using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PlatformerGame
{
	public class PlatformerGame : Game
	{
		public enum GamePartID
		{
			MainMenu,
			Game,
			SelectLevel,
			NextLevel,
			GameOver
		}

		IGamePart[] GameSections = new IGamePart[5];
		public GamePartID CurrentGameSection = 0;
		GraphicsDeviceManager graphicsManager;
		SpriteBatch spriteBatch;
		public int CurrentLevel;

		public PlatformerGame()
		{
			graphicsManager = new GraphicsDeviceManager(this);
			Settings.Resolution = new Point(800, 600);
			graphicsManager.PreferredBackBufferWidth = Settings.Resolution.X;
			graphicsManager.PreferredBackBufferHeight = Settings.Resolution.Y;

			Window.Title = "Coin Hunt";
			IsMouseVisible = true;
			Content.RootDirectory = "Data";
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Resources.GradientBackground = Texture2D.FromFile(GraphicsDevice, "Data\\LvlGradient.png");
			Resources.Tileset = Texture2D.FromFile(GraphicsDevice, "Data\\Tileset.png");
			Resources.Player = Texture2D.FromFile(GraphicsDevice, "Data\\Player.png");
			Resources.Spike = Texture2D.FromFile(GraphicsDevice, "Data\\Spike.png");
			Resources.Enemy_Unarmed = Texture2D.FromFile(GraphicsDevice, "Data\\Enemy.png");
			Resources.Enemy_Jumper = Texture2D.FromFile(GraphicsDevice, "Data\\JumperEnemy.png");
			Resources.Enemy_Sword = Texture2D.FromFile(GraphicsDevice, "Data\\EnemySword.png");
			//Resources.Enemy_Archer = Texture2D.FromFile(GraphicsDevice, "Data\\Spike.png");
			Resources.Coin = Texture2D.FromFile(GraphicsDevice, "Data\\Coin.png");
			Resources.Font = Content.Load<SpriteFont>("Arial_12px");
			Resources.ReloadLevels();

			GameSections[0] = new MainMenu();
			GameSections[1] = new MainGame();
			GameSections[2] = new SelectLevelScreen();
			GameSections[3] = new LevelCompleteScreen();
			GameSections[4] = new GameOverScreen();

			for (int i = 0; i < 3; i++)
				GameSections[i].Load(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			GameSections[(int)CurrentGameSection].Update(dt);
		}

		protected override void Draw(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			GameSections[(int)CurrentGameSection].Draw(spriteBatch,dt);
			spriteBatch.End();
		}

		public void SetResolution(Point r)
		{
			graphicsManager.PreferredBackBufferWidth = r.X;
			graphicsManager.PreferredBackBufferHeight = r.Y;
			graphicsManager.ApplyChanges();
		}

		protected override void UnloadContent()
		{
			for (int i = 0; i < 3; i++)
				GameSections[i].Unload(Content);
		}
	}
}
