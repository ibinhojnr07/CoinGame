using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
	class SelectLevelScreen : IGamePart
	{
		const int NumOfLevels = 4;

		Texture2D[] lvlBtns = new Texture2D[NumOfLevels];
		Button[] lvlButtons = new Button[NumOfLevels];

		public void Draw(SpriteBatch spriteBatch, float dt)
		{
			spriteBatch.Draw(Resources.GradientBackground, new Rectangle(0, 0, Settings.Resolution.X, Settings.Resolution.Y), Color.Green);

			for(int i = 0; i <lvlBtns.Length; i++) 
			{
				lvlButtons[i].Draw(spriteBatch);
			}
		}

		public void Load(ContentManager content)
		{
			GraphicsDevice gd = Program.game.GraphicsDevice;
			for (int i = 0; i < lvlBtns.Length; i++)
			{
				lvlBtns[i] = Texture2D.FromFile(gd, "Data\\btnLvl" + (i + 1) + ".png");
				int div = Settings.Resolution.X / lvlBtns.Length;
				lvlButtons[i] = new Button(lvlBtns[i], new Rectangle( div * i,0, (int)(div * 0.9), 100));
			}
		}

		public void Unload(ContentManager content)
		{
		
		}

		public void Update(float dt)
		{
			for (int i = 0; i < lvlBtns.Length; i++)
			{
				lvlButtons[i].Update(Mouse.GetState());

				if (lvlButtons[i].Clicked)
				{
					Program.game.CurrentLevel = i;
					Resources.ReloadLevels();
					Program.game.CurrentGameSection = PlatformerGame.GamePartID.Game;
				}
			}
		}
	}
}
