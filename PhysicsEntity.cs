using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformerGame
{
	abstract class PhysicsEntity
	{
		public PhysicsEntity()
		{
			forces = new List<Vector2>();
		}
		protected bool onGround;
		protected Vector2 position;
		protected Vector2 oldPosition;
		public abstract RectangleF Hitbox { get; }

		protected Vector2 _cameraPos;
		protected List<Vector2> forces;
		public virtual void CheckCollisions()
		{
			IEnumerable<Block> blocks = Resources.Levels[Program.game.CurrentLevel].EntityIntersectsBlocks(this, _cameraPos);

			// If the entity intersects a block, make him stand on the block:
			if (blocks.Any())
			{
				foreach (Block b in blocks)
				{
					if (b.Type != BlockType.Air)
					{
						if (!float.IsNaN(Direction.X) && !float.IsNaN(Direction.Y))
						{
							onGround = Vector2.Dot(Direction, -Vector2.UnitY) >= -0.9f;
						}
						else onGround = true;
						Collision(b);
					}
				}
			}
			else onGround = false;
		}

		public void ApplyForce(Vector2 f)
		{
			forces.Add(f);
		}

		public Vector2 Direction
		{
			get
			{
				Vector2 dp = oldPosition - position;
				if (dp.Length() != 0.0f)
				{
					dp.Normalize();
					return dp;
				}
				else return Vector2.UnitY;
			}
		}
		
		public void Collision(IEnemy b)
		{
			Vector2 dp = oldPosition - position;
			if (dp.Length() == 0) return; //Normalizing a 0-lenght vector returns a NaN value.
			dp.Normalize();

			float dst = Hitbox.Distance(b.Hitbox);
			position += dp * dst;
		}
		
		public void Collision(Block b)
		{
			Vector2 dp = oldPosition - position;
			if (dp.Length() == 0) return; //Normalizing a 0-lenght vector returns a NaN value.
			dp.Normalize();

			float dst = Hitbox.Distance(b.Hitbox);
			position = oldPosition + (dp * dst);
			position.Y = (float)Math.Round(position.Y);
		}
		public Vector2 ThrowDirection
		{
			get
			{
				if (!float.IsNaN(Direction.X) && !float.IsNaN(Direction.Y)) return Direction;
				else return Vector2.UnitY;
			}
		}

		public virtual void Update(float dt)
		{
			//Apply forces over the entity.
			List<int> _forcesToRemove = new List<int>(forces.Count);
			for (int i = 0; i < forces.Count; i++)
			{
				position += forces[i] * dt; //Explicit Euler integration : https://gafferongames.com/post/integration_basics/ 
				forces[i] *= 0.9f; //Forces decrease over time. (air resistance, for example)

				//Forces too small are removed.
				if (forces[i].Length() <= 0.01f) _forcesToRemove.Add(i);
			}
			foreach (int index in _forcesToRemove)
			{
				if (index < forces.Count) forces.RemoveAt(index);
			}

			Vector2 dir = position - oldPosition;
			if (dir.Length() == 0) return;
			else
			{
				dir.Normalize();
			}
		}

		public void Teleport(Vector2 pos)
		{
			position = pos;
			oldPosition = pos + Vector2.UnitY;
			forces.Clear();
		}
	}
}
