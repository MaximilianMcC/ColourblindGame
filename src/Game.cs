using System.Numerics;
using Raylib_cs;

class Game : Scene
{
	public override void Init()
	{
		Camera = new Camera2D(Raylib.GetScreenCenter(), Raylib.GetScreenCenter(), 0f, 1f);

		GameObjects.Add(new Platform(Vector2.Zero));
		GameObjects.Add(new Player(new Vector2(0, -50)));
	}
}