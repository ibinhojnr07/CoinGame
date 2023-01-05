using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PlatformerGame
{
	class MainMenu : IGamePart
	{
		Texture2D background;

		Texture2D playBtnT;
		Button PlayButton;

		Texture2D selectBtnT;
		Button SelectLevel;

		Texture2D quitBtnT;
		Button QuitButton;
		
		public void Draw(SpriteBatch spriteBatch, float dt)
		{
			spriteBatch.Draw(background, new Rectangle(0, 0, Settings.Resolution.X, Settings.Resolution.Y), Color.White);

			PlayButton.Draw(spriteBatch);
			SelectLevel.Draw(spriteBatch);
			QuitButton.Draw(spriteBatch);
		}

		public void Load(ContentManager content)
		{
			GraphicsDevice gd = Program.game.GraphicsDevice;
			background = Texture2D.FromFile(gd,"Data\\MainMenuBkg.png");

			playBtnT = Texture2D.FromFile(gd, "Data\\PlayButton.png");
			PlayButton = new Button(playBtnT, new Rectangle(50,200,200,50));

			selectBtnT = Texture2D.FromFile(gd, "Data\\SelectLevel.png");
			SelectLevel = new Button(selectBtnT, new Rectangle(50, 310, 200, 50));

			quitBtnT = Texture2D.FromFile(gd, "Data\\Quit.png");
			QuitButton = new Button(quitBtnT, new Rectangle(50, 420, 200, 50));
		}

		public void Unload(ContentManager content)
		{
			background.Dispose();
			playBtnT.Dispose();
			selectBtnT.Dispose();
			quitBtnT.Dispose();
		}

		public void Update(float dt)
		{
			PlayButton.Update(Mouse.GetState());
			if(PlayButton.Clicked)
			{
				Program.game.CurrentLevel = 0;
				Resources.ReloadLevels();
				Program.game.CurrentGameSection = PlatformerGame.GamePartID.Game;
			}
			SelectLevel.Update(Mouse.GetState());
			if(SelectLevel.Clicked)
			{
				Program.game.CurrentGameSection = PlatformerGame.GamePartID.SelectLevel;
			}
			QuitButton.Update(Mouse.GetState());
			if(QuitButton.Clicked)
			{
				Program.game?.Exit();
			}
		}
	}
}
