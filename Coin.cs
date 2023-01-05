using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
	class Coin
	{
		public Vector2 Position;
		public Coin(Vector2 pos) 
		{
			Position = pos;
			animTimer= 0;
		}

		public RectangleF Hitbox => new RectangleF(Position.X, Position.Y, 1, 1);

		float animTimer;
		public void Draw(SpriteBatch spriteBatch,float dt,Vector2 CameraPos)
		{
			Resources.DrawInGrid(spriteBatch, Resources.Coin, Hitbox, new Rectangle(((int)(animTimer / 0.25f) % 2) * 16, 0, 16, 32), CameraPos);
			animTimer += dt;
		}
	}
}
