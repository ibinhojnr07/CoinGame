using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	class Arrow : IEnemy
	{
		public Arrow(Vector2 initialPos,Vector2 velocity)
		{
			Position = initialPos;
			Rotation = (float)Math.Atan2(velocity.Y, velocity.X);
			_alive = true;
		}

		public Vector2 Position { get; set; }

		public RectangleF Hitbox => new RectangleF(Position.X, Position.Y, 0.5f, 0.5f);

		public Vector2 Velocity;

		bool _alive;
		
		
		
		public bool Alive
		{
			get => _alive;
		}
		public int Health => int.MaxValue;

		public bool CanBeStomped => false;

		public float Rotation;

		public void Draw(float dt, SpriteBatch spriteBatch, Vector2 cameraPos)
		{
			Resources.DrawInGrid(spriteBatch, Resources.Arrow, Hitbox, Rotation, Resources.Arrow.Bounds, cameraPos);
		}

		public PlayerEnemyContactInteraction OnContactWithPlayer(Player p, bool from_above)
		{
			_alive = false;
			return PlayerEnemyContactInteraction.TakeDamageAndThrow_Player;
		}

		public ProjectileInteraction OnContactWithProjectile()
		{
			return ProjectileInteraction.Ignore;
		}

		public void Update(float dt)
		{
			_alive = Position.Y >= 0;

			Position += Velocity * dt;
			Velocity *= 0.99f;
			Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);

			Position += Vector2.UnitY * 10.0f * dt; // Gravity
		}
		public void Throw()
		{
		}
		public void Kill()
		{
			_alive = false;
		}
		public void Hurt()
		{
		}
	}
}
