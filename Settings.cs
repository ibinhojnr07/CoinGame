using Microsoft.Xna.Framework;
using System;

namespace PlatformerGame
{
	static class Settings
	{
		static Point _resolution;
		public static Point Resolution
		{
			get => _resolution;
			set
			{
				Program.game?.SetResolution(value);
				_resolution = value;
			}
		}

		public static Point TilesPerScreen = new Point(20, 15);
	}
}
