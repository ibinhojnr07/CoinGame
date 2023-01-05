using System;

public class Program
{
	public static PlatformerGame.PlatformerGame? game = null;
	public static void Main()
	{
		using( game = new PlatformerGame.PlatformerGame())
		{
			game.Run();
		}
	}
}