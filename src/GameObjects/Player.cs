using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float acceleration = 2000f;
	private readonly float maxSpeed = 250f;
	private readonly NFloat frictionCoefficient = 0.1f;
	private readonly float jumpForce = 300f;

	private bool JumpingRn = false;

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
		// Make the camera track us
		SceneManager.Scene.Camera.Target = Hitbox.Position;

		// Move left/right
		float movement = acceleration * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) Velocity.X -= movement;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) Velocity.X += movement;

		// Don't let us speed
		Velocity.X = Math.Clamp(Velocity.X, -maxSpeed, maxSpeed);

		// Add friction
		Velocity.X *= MathF.Pow(1f - frictionCoefficient, Raylib.GetFrameTime() * 60f);
		if (MathF.Abs(Velocity.X) < 0.1f) Velocity.X = 0f;

		// Check for if we'd like to jump
		// TODO: Make a special foot collider for this
		if (Raylib.IsKeyPressed(KeyboardKey.Space) && IsBeingCollidedWith)
		{
			Velocity.Y = -jumpForce;
		}
	}

	public override void RenderUi()
	{
		Raylib.DrawText($"{Position:f2}\n{Velocity:f2}", 10, 10, 30, Color.White);
	}
}