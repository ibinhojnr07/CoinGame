using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerGame
{
	class SwordEnemy : WalkingEnemy
	{
		public SwordEnemy(Vector2 pos) : base(pos) 
		{
			_health = 2;
		}

		public override void Draw(float dt, SpriteBatch spriteBatch, Vector2 cameraPos)
		{
			if (Alive)
			{
				Rectangle source = new Rectangle(((int)(walkingAnimTimer / 0.1f) % 3) * 32, 0, 32, 64);
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Sword, Hitbox, 0.0f, source, cameraPos, Color.White, false, true);

				float localX = (position.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - cameraPos.X;
				_beingRendered = localX < Settings.Resolution.X;
				_cameraPos = cameraPos;
			}
			else
			{
				Resources.DrawInGrid(spriteBatch, Resources.Enemy_Sword, Hitbox, new Rectangle(0, 64, 32, 64), cameraPos);
			}
		}
		public override PlayerEnemyContactInteraction OnContactWithPlayer(Player p, bool from_above)
		{
			return PlayerEnemyContactInteraction.TakeDamageAndThrow_Player;
		}
	}
}
