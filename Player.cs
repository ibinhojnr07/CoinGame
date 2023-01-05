using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PlatformerGame
{
	enum PlayerStance
	{
		Staying,
		Running,
		Jumping,
		OnStairs,
		Aiming,
		Dead,
	}

	class Player : PhysicsEntity
	{
		public Player(Vector2 p) : base()
		{
			position = p;
			oldPosition = position + Vector2.UnitY;
			health = 3;
			Stance = PlayerStance.Staying;
			immunityTimer = 0.0f;
		}		
		public override RectangleF Hitbox
		{
			get
			{
				return new RectangleF(position.X, position.Y, 1.0f, 2.0f);
			}
		}

		public Vector2 Position { get => position; }

		uint health;
		PlayerStance _stance;
		public PlayerStance Stance
		{
			get => _stance;
			private set
			{
				walkingAnimTimer = 0.0f;
				shootingAnimTimer = 0.0f;
				_stance = value;
			}
		}
		float walkingAnimTimer;
		float shootingAnimTimer;
		float immunityTimer;

		public uint Health
		{
			get
			{
				return health;
			}
		}

		public bool Alive
		{
			get
			{
				return health != 0;
			}
		}
		bool _onStairs;
		public bool OnStairs
		{
			get => _onStairs;
		}

		bool flipDir = false;
		float jumpCooldown = 0.0f;

		public void Draw(float dt,SpriteBatch spriteBatch,Vector2 cameraPosition)
		{
			if(Alive)
			{
				RectangleF d = new RectangleF(position.X,position.Y,1,2);
				Color plrColor = immunityTimer >= 0.0f ? Color.Red : Color.White;

				switch (Stance)
				{
					case PlayerStance.Staying:
						Resources.DrawInGrid(
							spriteBatch, 
							Resources.Player,
							d,
							0.0f,
							new Rectangle(0, 0, 32, 64),
							cameraPosition,
							plrColor,
							false,
							flipDir);
						break;
					case PlayerStance.Running:
						walkingAnimTimer += dt;
						Resources.DrawInGrid(
							spriteBatch,
							Resources.Player,
							d,
							0.0f,
							new Rectangle(((int)(walkingAnimTimer / 0.1f) % 3) * 32, 64, 32, 64),
							cameraPosition,
							plrColor,
							false,
							flipDir
						);
						break;
					case PlayerStance.Jumping:
						walkingAnimTimer += dt;
						Resources.DrawInGrid(
							spriteBatch,
							Resources.Player,
							d,
							0.0f,
							new Rectangle(0, 0, 32, 64), 
							cameraPosition,
							plrColor,
							false,
							flipDir
						);
						break;
					case PlayerStance.Aiming:
						shootingAnimTimer += dt;
						Resources.DrawInGrid(
							spriteBatch,
							Resources.Player,
							d,
							0.0f,
							new Rectangle(Math.Max((int)(shootingAnimTimer / 0.5f), 3) * 32, 128, 32, 64),
							cameraPosition,
							plrColor,
							false,
							flipDir
						);
						break;
					case PlayerStance.Dead:
						Resources.DrawInGrid(spriteBatch, Resources.Player, d, new Rectangle(32, 0, 32, 64), cameraPosition);
						break;
				}
				_cameraPos = cameraPosition;
			}
		}

		public override void CheckCollisions()
		{
			IEnumerable<Block> blocks = Resources.Levels[Program.game.CurrentLevel].PlayerIntersectsBlocks(this,_cameraPos);

			// If the player intersects a block, make him stand on the block:
			if (blocks.Any())
			{
				foreach (Block b in blocks)
				{
					if (b.Type != BlockType.Air)
					{
						if (b.Type == BlockType.Ladder) _stance = PlayerStance.OnStairs;
						else
						{
							_stance = PlayerStance.Running;
							Vector2 collisionDir = b.Hitbox.Middle - position;
							collisionDir.Normalize();

							float dst = new RectangleF(oldPosition.X, oldPosition.Y, Hitbox.Width, Hitbox.Height).Distance(b.Hitbox);
							position = oldPosition - collisionDir * dst;
							position.Y = (float)Math.Round(position.Y);
							if ((int)oldPosition.Y == (int)position.Y) onGround = true;
							
							//Eliminate forces that force the playe
							for(int i = 0; i < forces.Count; i++)
							{
								if (Vector2.Dot(collisionDir, forces[i]) >= 0.5f)
									forces[i] = Vector2.Zero;
							}

							if (collisionDir.Y >= 0.0f) onGround = true;
							
						}
					}
				}
			}
			else onGround = false;
		}

		public void Update(float dt)
		{
			KeyboardState keyboard = Keyboard.GetState();

			bool inp_jump = keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W);
			bool inp_left = keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A);
			bool inp_right = keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D);
			bool inp_down = keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S);

			if (Alive)
			{
				if (Stance == PlayerStance.OnStairs)
				{
					_onStairs = true;
					if (inp_left) position.X -= 5 * dt;
					if( inp_right) position.X += 5 * dt;
					if (inp_jump) position.Y -= 7 * dt;
					if (inp_down) position.Y += 7 * dt;
				}
				else
				{
					if (onGround)
					{
						if (inp_jump && jumpCooldown <= 0.0f)
						{
							_stance = PlayerStance.Jumping;
							forces.Add(new Vector2(0, -50));
							jumpCooldown = 0.5f;
						}
						if (inp_left) _stance = PlayerStance.Running;
						if (inp_right) _stance = PlayerStance.Running;
					}
				}

				if (_stance != PlayerStance.OnStairs && !onGround) position.Y += 10 * dt; //gravity

				CheckCollisions();

				switch (Stance)
				{
					case PlayerStance.Staying:
					case PlayerStance.Running:
					case PlayerStance.Jumping:
						HandleLateralMovment(dt, inp_left, inp_right);
						break;
					case PlayerStance.Aiming:
						break;
					case PlayerStance.Dead:
						Vector2 lf = forces[forces.Count - 1];
						forces.Clear();
						forces.Add(new Vector2(lf.X, Math.Abs(lf.Y) * 1.5f));
						break;
				}

				if (!inp_left && !inp_right && !inp_jump && !OnStairs)
				{
					_stance = PlayerStance.Staying;
				}

				if (immunityTimer >= 0.0f) immunityTimer -= dt;
				if(jumpCooldown >= 0.0f) jumpCooldown -= dt;

				base.Update(dt);
				flipDir = oldPosition.X - position.X > 0;
				if (position != oldPosition)
					oldPosition = position;
			}
		}

		public void Hurt()
		{
			if(immunityTimer <= 0.0f)
			{
				health -= 1;
				immunityTimer = 1f;
			}
		}

		public void Kill()
		{
			health = 0;
		}

		void HandleLateralMovment(float dt,bool left,bool right)
		{
			if (left)
			{
				oldPosition = position;
				position.X -= 7 * dt;
				CheckCollisions();
			}
			if (right)
			{
				oldPosition = position;
				position.X += 7 * dt;
				CheckCollisions();
			}
		}
	}
}
