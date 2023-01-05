using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	class JumpingEnemy : WalkingEnemy
	{
		float _jumpingTimer;
		public JumpingEnemy(Vector2 pos) : base(pos) 
		{
			_jumpingTimer = 0.0f;
		}

		public override void Draw(float dt, SpriteBatch spriteBatch, Vector2 cameraPos)
		{
			if (Alive)
			{
				Rectangle source = new Rectangle(((int)(walkingAnimTimer / 0.1f) % 3) * 32, 0, 32, 64);
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Jumper, Hitbox, 0.0f, source, cameraPos, Color.White, false, true);

				float localX = (position.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - cameraPos.X;
				_beingRendered = localX > 0 && localX < Settings.Resolution.X;
				_cameraPos = cameraPos;
			}
			else
			{
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Jumper, Hitbox, new Rectangle(0, 64, 32, 64), cameraPos);
			}
		}

		public override void Update(float dt)
		{
			if (Alive)
			{
				position.X -= dt; //Decrease the speed twice
				_jumpingTimer += dt;
				if (_jumpingTimer >= 2.5f && _beingRendered)
				{
					ApplyForce(new Vector2(0, -75.0f));
					_jumpingTimer = 0.0f;
				}
			}
			base.Update(dt); //Use the same behiavour as the Walking enemy
		}
	}
}
