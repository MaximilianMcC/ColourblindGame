using System.Numerics;
using Raylib_cs;

class Game : Scene
{
	public override void Init()
	{
		Camera = new Camera2D(Raylib.GetScreenCenter(), Raylib.GetScreenCenter(), 0f, 1f);

		Player = new Player(new Vector2(0, -100));
		GameObjects.Add(Player);

		GameObjects.Add(new Platform(Vector2.Zero));
		GameObjects.Add(new Box(new Vector2(128, -64)));
		GameObjects.Add(new Box(new Vector2(300, -64*2)));
		GameObjects.Add(new FallingPlatform(new Vector2(-200, 0)));
		GameObjects.Add(new MovingPlatform(new Vector2(600, 0)));
	}
}