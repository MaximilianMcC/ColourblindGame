using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float acceleration = 2000f;
	private readonly float maxSpeed = 300f;
	private NFloat frictionCoefficient = 0.2f;
	private readonly float jumpForce = 300f;

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

		// See what direction we're moving in
		int movementDirection = (-Utils.BoolI(Raylib.IsKeyDown(KeyboardKey.Left))) + Utils.BoolI(Raylib.IsKeyDown(KeyboardKey.Right));

		// Move left/right
		float movement = acceleration * Raylib.GetFrameTime();
		Velocity.X += movement * movementDirection;

		// Don't let us speed
		Velocity.X = Math.Clamp(Velocity.X, -maxSpeed, maxSpeed);

		// Add friction if we're not moving
		// TODO: Air friction
		if (movementDirection == 0)
		{
			Velocity.X *= MathF.Pow(1f - frictionCoefficient, Raylib.GetFrameTime() * 60f);
			if (MathF.Abs(Velocity.X) < 0.1f) Velocity.X = 0f;
		}

		// Check for if we'd like to jump
		// TODO: Make a special foot collider for this
		if ((Raylib.IsKeyPressed(KeyboardKey.Space) || Raylib.IsKeyPressed(KeyboardKey.Up)) && IsBeingCollidedWith)
		{
			Velocity.Y = -jumpForce;
		}
	}

	public override void RenderUi()
	{
		Raylib.DrawText($"{Position:f2}\n{Velocity:f2}", 10, 10, 30, Color.White);
	}
}