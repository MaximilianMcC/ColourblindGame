using System.Numerics;
using Raylib_cs;

class MovingPlatform : Platform
{
	private readonly float distance = 300f;
	private readonly NFloat direction = 1f;
	private readonly float speed = 100f;

	private float currentDirection;

	private Vector2 startPosition;
	private Vector2 endPosition;

	public MovingPlatform(Vector2 position) : base(position)
	{
		Texture = new Texture("./assets/sliding-platform.png");
		Transform.Size = new Vector2(Texture.Width, Texture.Height);

		startPosition = position;
		endPosition = startPosition + (Vector2.UnitX * (distance * direction));

		currentDirection = direction;
	}

	public override void Update()
	{
		ActAsPositionalParentForThingsCollidingWithUs();

		// Move
		Transform.Position.X += (currentDirection * speed) * Raylib.GetFrameTime();
		if (Transform.Position.X < startPosition.X || Transform.Position.X > endPosition.X) currentDirection *= -1;
	}
}