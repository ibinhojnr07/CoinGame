using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PlatformerGame
{
	internal class Level
	{
		List<Block> Blocks;
		List<IEnemy> Enemies;
		List<Coin> Coins;
		public Vector2 PlayerDefaultPos;
		bool levelCompleted = false;

		public int TotalCoins
		{
			get => Coins.Count;
		}
		int _coinsLeft = 0;
		public int CoinsLeft
		{
			get => _coinsLeft;
		}
		public bool LevelCompleted
		{
			get => levelCompleted;
		}

		private Level()
		{
			Blocks = new List<Block>();
			Enemies = new List<IEnemy>();
			Coins = new List<Coin>();
			PlayerDefaultPos = Vector2.Zero;
		}

		public static Level FromFile(string file)
		{
			Level l = new Level();

			if (File.Exists(file))
			{
				string[] lines = File.ReadAllLines(file);
				foreach (string line in lines)
				{
					bool inv = false;
					if (string.IsNullOrEmpty(line)) continue;
					for (int i = 0; i < line.Length; i++)
					{
						if (line[i] == '#') inv = true;
					}
					if (inv) continue;

					string[] data = line.Split(new char[] { ' ' });

					CultureInfo c = CultureInfo.InvariantCulture;

					int type1 = Convert.ToInt32(data[0], c);
					int type2 = Convert.ToInt32(data[1], c);
					float x = Convert.ToSingle(data[2], c);
					float y = Convert.ToSingle(data[3], c);

					switch(type1)
					{
						case 0:
							l.Blocks.Add(new Block((BlockType)type2, new Vector2(x, y)));
							break;
						case 1:
							switch (type2)
							{
								case 0:
									{
										l.Enemies.Add(new Spike(new Vector2(x, y), Convert.ToInt32(data[4], c)));
										break;
									}
								case 1:
									l.Enemies.Add(new WalkingEnemy(new Vector2(x, y)));
									break;
								case 2:
									l.Enemies.Add(new JumpingEnemy(new Vector2(x, y)));
									break;
								case 3:
									l.Enemies.Add(new SwordEnemy(new Vector2(x, y)));
									break;
								default:
									break;
							}
							break;
						case 2:
							l.PlayerDefaultPos = new Vector2(x, y);
							break;
						case 3:
							l.Coins.Add(new Coin(new Vector2(x,y)));
							break;
						default:
							break;

					}
				}
			}
			return l;
		}

		public void Update(Player player, float dt, Vector2 CameraPos)
		{
			foreach (IEnemy entity in Enemies)
			{
				entity.Update(dt);

				if (player.Hitbox.Intersects(entity.Hitbox))
				{
					Vector2 plrAttackDir = player.Hitbox.Middle - entity.Hitbox.Middle;
					plrAttackDir.Normalize();

					bool above = Vector2.Dot(plrAttackDir, -Vector2.UnitY) > 0.5f;
					int prevHealth = entity.Health;
					player.Collision(entity);
					switch (entity.OnContactWithPlayer(player, above))
					{
						case PlayerEnemyContactInteraction.Ignore:
							break;
						case PlayerEnemyContactInteraction.ActAsSolid:
							player.Collision(entity);
							break;
						case PlayerEnemyContactInteraction.TakeDamage_Player:
							player.Hurt();
							break;
						case PlayerEnemyContactInteraction.TakeDamageAndThrow_Player:
							player.Hurt();
							player.ApplyForce(plrAttackDir * 5.0f * entity.Health);
							break;
						case PlayerEnemyContactInteraction.Kill_Player:
							player.Kill();
							break;
						case PlayerEnemyContactInteraction.TakeDamage_Enemy:
							entity.Hurt();
							break;
						case PlayerEnemyContactInteraction.TakeDamageAndThrow_Enemy:
							entity.Hurt();
							entity.Throw();
							break;
						case PlayerEnemyContactInteraction.Kill_Enemy:
							entity.Kill();
							break;
					}
					if (above && entity.Health < prevHealth)
					{
						player.ApplyForce(-player.ThrowDirection * 40.0f);
					}
				}
			}
			//Update all coins, and check if they're all collected
			levelCompleted = true;
			_coinsLeft = Coins.Count;
			for (int i = 0; i < Coins.Count; i++)
			{
				if (Coins[i] != null)
				{
					levelCompleted = false;
					if (player.Hitbox.Intersects(Coins[i].Hitbox))
					{
						Coins[i] = null;
					}
					else --_coinsLeft;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, float dt, Vector2 CameraPos)
		{
			foreach (Block block in Blocks)
			{
				block.Draw(spriteBatch, CameraPos);
			}
			foreach (IEnemy entity in Enemies)
			{
				entity?.Draw(dt, spriteBatch, CameraPos);
			}
			foreach(Coin coin in Coins)
			{
				coin?.Draw(spriteBatch,dt, CameraPos);
			}
		}

		public IEnumerable<Block> PlayerIntersectsBlocks(Player p, Vector2 cameraPos)
		{
			foreach (Block b in Blocks)
			{
				float localX = (b.Position.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - cameraPos.X;
				bool _beingRendered = localX >= 0 && localX <= Settings.Resolution.X;

				if (_beingRendered)
				{
					if (b.Type != BlockType.Ladder && p.Hitbox.Intersects(b.Hitbox))
					{
						yield return b;
					}
				}
			}
			yield break;
		}

		public IEnumerable<Block> EntityIntersectsBlocks(PhysicsEntity p, Vector2 cameraPos)
		{
			foreach (Block b in Blocks)
			{
				float localX = (b.Position.X * Settings.Resolution.X / Settings.TilesPerScreen.X) - cameraPos.X;
				bool _beingRendered = localX >= 0 && localX < Settings.Resolution.X;

				//if (_beingRendered)
				//{
					if (b.Type != BlockType.Ladder && p.Hitbox.Intersects(b.Hitbox))
					{
						yield return b;
					}
				//}
			}
		}
	}
}
