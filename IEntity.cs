using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
	public interface IEntity
	{
		void Draw(float dt, SpriteBatch spriteBatch,Vector2 cameraPos);
		void Update(float dt);
	}
}