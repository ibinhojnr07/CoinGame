using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
	class GameOverScreen : IGamePart
	{
		float _screenTime = 0.0f;

		public void Draw(SpriteBatch spriteBatch, float dt)
		{
			spriteBatch.Draw(Resources.GradientBackground, new Rectangle(0, 0, 800, 600), Color.Red);

			Vector2 textSize = Resources.Font.MeasureString("Game over!");
			spriteBatch.DrawString(Resources.Font, "Game over!", new Vector2((Settings.Resolution.X - textSize.X) / 2, (Settings.Resolution.Y - textSize.Y) / 2), Color.Red);

		}

		public void Load(ContentManager content)
		{
			
		}

		public void Unload(ContentManager content)
		{
			
		}

		public void Update(float dt)
		{
			_screenTime += dt;
			if(_screenTime >= 5.0f)
			{
				Program.game.CurrentGameSection = PlatformerGame.GamePartID.MainMenu; //send the player back to the main menu
				_screenTime = 0.0f;
			}
		}
	}
}
