using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace PlatformerGame
{
	class MainGame : IGamePart
	{
		Texture2D heart;
		Vector2 cameraPos;
		Player player;

		Song song;
		bool _songPlaying = false;

		public MainGame()
		{
			cameraPos = new Vector2(0, 0);
		}

		public void Load(ContentManager content)
		{
			GraphicsDevice gd = Program.game.GraphicsDevice;
			heart = Texture2D.FromFile(gd, "Data\\Heart.png");

			int currentLevel = Program.game.CurrentLevel;
			player = new Player(Resources.Levels[currentLevel].PlayerDefaultPos);

			song = content.Load<Song>("Fastrun1");
		}

		public void Draw(SpriteBatch spriteBatch, float dt)
		{	
			cameraPos = new Vector2(0, 0);
			cameraPos.X = Math.Max((player.Position.X - Settings.TilesPerScreen.X / 2 ) * Settings.Resolution.X / Settings.TilesPerScreen.X, 0);

			spriteBatch.Draw(Resources.GradientBackground, new Rectangle(0, 0, Settings.Resolution.X, Settings.Resolution.Y), Color.White);
			player.Draw(dt, spriteBatch, cameraPos);

			int currentLevel = Program.game.CurrentLevel;
			Resources.Levels[currentLevel].Draw(spriteBatch, dt, cameraPos);

			for(int i = 0; i < player.Health; i++)
			{
				spriteBatch.Draw(heart, new Rectangle(35 * i, 0, 30, 30),Color.White);
			}

			spriteBatch.DrawString(
				Resources.Font,
				string.Format("Coins : {0}/{1}", Resources.Levels[currentLevel].CoinsLeft, Resources.Levels[currentLevel].TotalCoins),
				new Vector2(110,0),
				Color.White
			);
		}

		public void Update(float dt)
		{
			int currentLevel = Program.game.CurrentLevel;

			Level currLevel = Resources.Levels[currentLevel];
			currLevel.Update(player, dt, cameraPos);

			if (currLevel.LevelCompleted)
			{
				if (currentLevel + 1 == Resources.Levels.Length)
				{
					Program.game.CurrentGameSection = PlatformerGame.GamePartID.MainMenu;
					MediaPlayer.Stop();
					_songPlaying = false;
					return;
				}

				Program.game.CurrentGameSection = PlatformerGame.GamePartID.NextLevel; //Switch to 'level complete' screen 
				player = new Player(Resources.Levels[currentLevel + 1].PlayerDefaultPos);
			}

			if (!player.Alive)
			{
				Program.game.CurrentGameSection = PlatformerGame.GamePartID.GameOver; //Switch to 'game over' screen
				player = new Player(Resources.Levels[currentLevel].PlayerDefaultPos);
				_songPlaying = false;
				MediaPlayer.Stop();
			}
			if(player.Position.Y >= 16.0f) //If player fell under the map:
			{
				player.Hurt();
				if (player.Alive) player.Teleport(Resources.Levels[currentLevel].PlayerDefaultPos);
			}
			player.Update(dt);

			if (!_songPlaying && player.Alive && Program.game.CurrentGameSection == PlatformerGame.GamePartID.Game)
			{
				MediaPlayer.Play(song);
				_songPlaying = true;
			}
			if(Program.game.CurrentGameSection != PlatformerGame.GamePartID.Game)
			{
				MediaPlayer.Stop();
				_songPlaying = false;
			}
		}
		public void Unload(ContentManager content)
		{
		}
	}
}
