using System.Numerics;
using Raylib_cs;

class FallingPlatform : Platform
{
	private readonly float timeToHover = 0.5f;
	private readonly float timeToFallFor = 3f;
	private readonly float timeToRespawn = 2f;

	public bool Falling => state != State.Dormant;
	private State state;
	private Timer timer = new Timer();

	private readonly Vector2 initialPosition;
	private Vector2 respawnPosition;

	public FallingPlatform(Vector2 position) : base(position)
	{
		initialPosition = position;
		Texture = Raylib.LoadTexture("./assets/falling-platform.png");
		Transform.Size = new Vector2(Texture.Width, Texture.Height);

		GravityMultiplier = 1.2f;
	}

	public override void Update()
	{
		// Check for if something touches the platform
		if (state == State.Dormant && IsBeingCollidedWith)
		{
			state = State.GoingToFall;
			timer.Start();
		}

		// Check for if we're going to fall
		if (state == State.GoingToFall && timer.RestartIfHasBeen(timeToHover))
		{
			state = State.CurrentlyFalling;
			HasGravity = true;
			HasCollisionDetection = false;
		}

		// Check for if we are going to respawn
		if (state == State.CurrentlyFalling && timer.RestartIfHasBeen(timeToFallFor))
		{
			HasGravity = false;
			respawnPosition = Transform.Position;
			state = State.Respawning;
		}

		// Check for if we are respawning
		if (state == State.Respawning)
		{
			// Lerp towards the initial position
			float lerpPercentage = timer.Time / timeToRespawn;
			Transform.Position = Vector2.Lerp(respawnPosition, initialPosition, lerpPercentage);

			// Check for if we've finished respawning
			if (timer.RestartIfHasBeen(timeToRespawn))
			{
				state = State.Dormant;
				HasCollisionDetection = true;
			}
		}
	}

	private enum State
	{
		Dormant,
		GoingToFall,
		CurrentlyFalling,
		Respawning
	}
}