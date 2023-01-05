using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	class WalkingEnemy : PhysicsEntity, IEnemy
	{
		public WalkingEnemy(Vector2 position)
		{
			this.position = position;
			_health = 1;
			walkingAnimTimer = 0.0f;
			oldPosition = position;
		}

		public Vector2 Position
		{
			get => position;
			set { position = value; }
		}

		public override RectangleF Hitbox => new RectangleF(position.X, position.Y, 1.0f, 2.0f);

		public bool Alive => _health > 0;

		protected int _health;
		public int Health => _health;
		protected float walkingAnimTimer;
		protected bool _beingRendered;

		public virtual void Draw(float dt, SpriteBatch spriteBatch, Vector2 cameraPos)
		{
			if (Alive)
			{
				Rectangle source = new Rectangle(((int)(walkingAnimTimer / 0.1f) % 3) * 32, 0, 32, 64);
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Unarmed, Hitbox,0.0f, source, cameraPos,Color.White,false,true);

				float localX = (position.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - cameraPos.X;
				_beingRendered = localX < Settings.Resolution.X;
				_cameraPos = cameraPos;
			}
			else
			{
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Unarmed, Hitbox, new Rectangle(0, 64, 32, 64), cameraPos);
			}
		}

		public void Hurt()
		{
			_health -= 1;
		}

		public void Kill()
		{
			_health = 0;
		}

		public virtual PlayerEnemyContactInteraction OnContactWithPlayer(Player p, bool from_above)
		{
			if (Alive) return from_above ? PlayerEnemyContactInteraction.TakeDamage_Enemy : PlayerEnemyContactInteraction.TakeDamageAndThrow_Player;
			else return PlayerEnemyContactInteraction.Ignore;
		}

		public ProjectileInteraction OnContactWithProjectile()
		{
			return Alive ? ProjectileInteraction.TakeDamage : ProjectileInteraction.Ignore;
		}

		public void Throw()
		{
			
		}

		public override void Update(float dt)
		{
			if (Alive)
			{
				walkingAnimTimer += dt;

				if (_beingRendered)
				{
					position.X -= dt;
					if (!onGround)
						position.Y += 10 * dt;

					CheckCollisions();
					oldPosition = position;
				}
				base.Update(dt);
			}
		}
	}
}
