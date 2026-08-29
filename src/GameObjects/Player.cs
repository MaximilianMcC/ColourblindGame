using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private float speed = 250f;

	public Player(Vector2 position)
	{
		Position = position;
		Texture = Raylib.LoadTexture("./assets/test.png");
		Hitbox.Size = new Vector2(64);

		HasCollisionDetection = true;
		HasCollisionResolution = true;
		HasGravity = true;
	}

	public override void Update()
	{
		SceneManager.Scene.Camera.Target = Hitbox.Position;

		float movement = speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) Position.X -= movement;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) Position.X += movement;
	}

	public override void RenderUi()
	{
		Raylib.DrawText($"{2f:Position}\n{2f:Velocity}", 10, 10, 30, Color.White);
	}
}