using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private float speed = 250f;

	public Player(Vector2 position)
	{
		Hitbox.Position = position;
		Texture = Raylib.LoadTexture("./assets/test.png");
		Hitbox.Size = new Vector2(64);
	}

	public override void Update()
	{
		SceneManager.Scene.Camera.Target = Hitbox.Position;

		float movement = speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) Position.X -= movement;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) Position.X += movement;
	}
}