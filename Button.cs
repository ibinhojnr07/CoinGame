using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
	class Button
	{
		public Button(Texture2D txt, Rectangle coords)
		{
			Texture = txt;
			Coordinates = coords;
		}

		public Texture2D Texture { get; private set; }

		public Rectangle Coordinates;

		bool _clicked;
		public bool Clicked
		{
			get
			{
				return _clicked;
			}
		}

		bool _hovered;
		public bool Hovered
		{
			get
			{
				return _hovered;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Coordinates, Color.White);
		}

		ButtonState _lastMouseState;

		public void Update(MouseState mouse)
		{
			_hovered = Coordinates.Contains(mouse.Position);
			_clicked = _hovered && mouse.LeftButton == ButtonState.Released && _lastMouseState == ButtonState.Pressed;
			_lastMouseState = mouse.LeftButton;
		}
	}
}