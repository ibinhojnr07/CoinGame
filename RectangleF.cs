using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;

namespace PlatformerGame
{
	struct RectangleF
	{
		public RectangleF(float x, float y, float w, float h)
		{
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		public float X, Y;
		public float Width, Height;

		public float Left => X;
		public float Right => X + Width;
		public float Top => Y;
		public float Bottom => Y + Height;

		public Vector2 Middle => new Vector2(X + Width/2, Y + Height/2);

		public bool Intersects(RectangleF r)
		{
			if (r.Left < Right && Left < r.Right && r.Top < Bottom)
			{
				return Top < r.Bottom;
			}
			return false;
		}
		public bool Intersects(Vector2 v)
		{
			return X >= v.X && v.X <= X + Width &&
				Y >= v.Y && v.Y <= Y + Height;
		}

		public float Distance(RectangleF r)
		{
			float x1 = X, y1 = Y, x1b = X + Width, y1b = Y + Height, x2 = r.X, y2 = r.Y, x2b = r.X + r.Width, y2b = r.Y + r.Height;
			bool left = x2b < x1;
			bool right = x1b < x2;
			bool bottom = y2b < y1;
			bool top = y1b < y2;

			if (top && left)
				return Vector2.Distance(new Vector2(x1, y1b), new Vector2(x2b, y2));

			else if (left && bottom)
				return Vector2.Distance(new Vector2(x1, y1), new Vector2(x2b, y2b));

			else if (bottom && right)
				return Vector2.Distance(new Vector2(x1b, y1), new Vector2(x2, y2b));

			else if (right && top)
				return Vector2.Distance(new Vector2(x1b, y1b), new Vector2(x2, y2));

			else if (left)
				return x1 - x2b;

			else if (right)
				return x2 - x1b;

			else if (bottom)
				return y1 - y2b;

			else if (top) 
				return y2 - y1b;

			else return 0;
		}

		public static implicit operator Rectangle(RectangleF r)
		{
			return new Rectangle(
				(int)r.X,
				(int) r.Y,
				(int)r.Width,
				(int)r.Height
			);
		}
	}
}