using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
	class Spike : IEnemy
	{
		public Spike(Vector2 pos,int r)
		{
			Position = pos;
			Rotation = r;
		}

		public RectangleF Hitbox => new RectangleF(Position.X, Position.Y, 1, 1);

		public Vector2 Position { get; set; }

		public bool Alive => true;
		public int Health => 5;

		public int Rotation;

		public void Draw(float dt, SpriteBatch spriteBatch,Vector2 cameraPos)
		{
			Resources.DrawInGridFlip(spriteBatch, Resources.Spike, Hitbox, Rotation, Resources.Spike.Bounds, cameraPos);
		}

		public PlayerEnemyContactInteraction OnContactWithPlayer(Player p, bool from_above)
		{
			return PlayerEnemyContactInteraction.TakeDamageAndThrow_Player;
		}

		public ProjectileInteraction OnContactWithProjectile()
		{
			return ProjectileInteraction.Ignore;
		}

		public void Update(float dt)
		{

		}

		public void Throw()
		{

		}

		public void Kill()
		{

		}

		public void Hurt()
		{

		}
	}
}
