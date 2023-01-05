using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	static class Resources
	{
		public static Texture2D? Tileset;
		public static Texture2D? Player;
		public static Texture2D? Spike;
		public static Texture2D? Enemy_Unarmed;
		public static Texture2D? Enemy_Jumper;
		public static Texture2D? Enemy_Sword;
		public static Texture2D? Enemy_Archer;
		public static Texture2D? Arrow;
		public static Texture2D? Coin;
		public static SpriteFont? Font;
		public static Texture2D? GradientBackground;

		public static Level[] Levels;

		public static void ReloadLevels()
		{
			Levels = new Level[]{
				Level.FromFile("Data\\Level1.txt"),
				Level.FromFile("Data\\Level2.txt"),
				Level.FromFile("Data\\Level3.txt"),
				Level.FromFile("Data\\Level4.txt")
			};
		}

		public static void DrawInGrid(SpriteBatch spriteBatch,Texture2D texture,RectangleF dest,Rectangle source,Vector2 CameraPos)
		{
			spriteBatch.Draw(texture,
				new Rectangle(
					(int)((dest.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - CameraPos.X),
					(int)((dest.Y * Settings.Resolution.Y / Settings.TilesPerScreen.Y) - CameraPos.Y),
					(int)(dest.Width * Settings.Resolution.Y / Settings.TilesPerScreen.Y),
					(int)(dest.Height * Settings.Resolution.Y / Settings.TilesPerScreen.Y)
					),
				source,
				Color.White);
		}

		public static void DrawInGrid(SpriteBatch spriteBatch, Texture2D texture, RectangleF dest,float rotationRadians, Rectangle source, Vector2 CameraPos)
		{
			spriteBatch.Draw(texture,
				new Rectangle(
					(int)((dest.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - CameraPos.X),
					(int)((dest.Y * Settings.Resolution.Y / Settings.TilesPerScreen.Y) - CameraPos.Y),
					(int)(dest.Width * Settings.Resolution.Y / Settings.TilesPerScreen.Y),
					(int)(dest.Height * Settings.Resolution.Y / Settings.TilesPerScreen.Y)
					),
				source,
				Color.White,
				rotationRadians,
				Vector2.Zero,
				SpriteEffects.None,
				0);
		}

		public static void DrawInGrid(SpriteBatch spriteBatch, Texture2D texture, RectangleF dest, float rotationRadians, Rectangle source, Vector2 CameraPos,Color color,bool vertFlip,bool horFlip)
		{
			spriteBatch.Draw(texture,
				new Rectangle(
					(int)((dest.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - CameraPos.X),
					(int)((dest.Y * Settings.Resolution.Y / Settings.TilesPerScreen.Y) - CameraPos.Y),
					(int)(dest.Width * Settings.Resolution.Y / Settings.TilesPerScreen.Y),
					(int)(dest.Height * Settings.Resolution.Y / Settings.TilesPerScreen.Y)
					),
				source,
				color,
				rotationRadians,
				Vector2.Zero,
				(vertFlip ? SpriteEffects.FlipVertically : SpriteEffects.None) | (horFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None),
				0);
		}

		public static void DrawInGridFlip(SpriteBatch spriteBatch, Texture2D texture, RectangleF dest, int r, Rectangle source, Vector2 CameraPos)
		{
			SpriteEffects eff;
			switch (r)
			{
				case 1:
					eff = SpriteEffects.FlipHorizontally;
					break;
				case 2: 
					eff = SpriteEffects.FlipVertically;
					break;
				case 3: 
					eff = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
					break;
				default:
					eff = SpriteEffects.None;
					break;
			}

			spriteBatch.Draw(texture,
				new Rectangle(
					(int)((dest.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - CameraPos.X),
					(int)((dest.Y * Settings.Resolution.Y / Settings.TilesPerScreen.Y) - CameraPos.Y),
					(int)(dest.Width * Settings.Resolution.Y / Settings.TilesPerScreen.Y),
					(int)(dest.Height * Settings.Resolution.Y / Settings.TilesPerScreen.Y)
					),
				source,
				Color.White,
				0,
				Vector2.Zero,
				eff,
				0);
		}
	}
}
