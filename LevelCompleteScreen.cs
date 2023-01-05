using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	class LevelCompleteScreen : IGamePart
	{
		float _levelChangeTimer = 0.0f;
		public void Load(ContentManager content) { }
		public void Draw(SpriteBatch spriteBatch, float dt)
		{
			spriteBatch.Draw(Resources.GradientBackground, new Rectangle(0, 0, 800, 600), Color.Gray);

			Vector2 textSize = Resources.Font.MeasureString("Level complete!");
			spriteBatch.DrawString(Resources.Font, "Level complete!", new Vector2((Settings.Resolution.X - textSize.X) / 2, (Settings.Resolution.Y - textSize.Y) / 2), Color.Black);
		}

		public void Unload(ContentManager content) { }
		public void Update(float dt)
		{
			_levelChangeTimer += dt;
			if(_levelChangeTimer > 5.0f)
			{
				_levelChangeTimer = 0.0f;
				++Program.game.CurrentLevel;
				Program.game.CurrentGameSection = PlatformerGame.GamePartID.Game; //Switch to MainGame, and play the next level.
			}
		}
	}
}
