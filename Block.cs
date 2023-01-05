using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
	enum BlockType : byte
	{
		Air = 0,
		GrassTL,
		GrassMT,
		GrassTR,
		Bricks,
		GrassML,
		Dirt,
		GrassMR,
		Wood,
		GrassBL,
		GrassMB,
		GrassBR,
		Ladder,
	}

	struct Block
	{
		public Vector2 Position;
		public BlockType Type;

		public Block(BlockType t,Vector2 p)
		{
			Position = p;
			Type = t;
		}

		static RectangleF GetBlockSource(BlockType t)
		{
			return t switch
			{
				BlockType.GrassTL => new RectangleF(0, 0, 32, 32),
				BlockType.GrassMT => new RectangleF(32, 0, 32, 32),
				BlockType.GrassTR => new RectangleF(64, 0, 32, 32),
				BlockType.Bricks => new RectangleF(96, 0, 32, 32),
				BlockType.GrassML => new RectangleF(0, 32, 32, 32),
				BlockType.Dirt => new RectangleF(32, 32, 32, 32),
				BlockType.GrassMR => new RectangleF(64, 32, 32, 32),
				BlockType.Wood => new RectangleF(96, 32, 32, 32),
				BlockType.GrassBL => new RectangleF(0, 64, 32, 32),
				BlockType.GrassMB => new RectangleF(32, 64, 32, 32),
				BlockType.GrassBR => new RectangleF(64, 64, 32, 32),
				BlockType.Ladder => new RectangleF(96, 64, 32, 32),
				_ => new RectangleF(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue),
			};
		}

		public RectangleF Hitbox => new RectangleF(Position.X, Position.Y, 1, 1);

		public void Draw(SpriteBatch spriteBatch,Vector2 CameraPos)
		{
			Resources.DrawInGrid(spriteBatch, Resources.Tileset, Hitbox, GetBlockSource(Type),CameraPos);
		}
	}
}