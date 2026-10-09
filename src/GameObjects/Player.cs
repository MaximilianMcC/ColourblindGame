using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	public Vector2 Spawnpoint;

	private readonly float acceleration = 2000f;
	private readonly float maxSpeed = 400f;
	private NFloat frictionCoefficient = 0.2f;
	private readonly float jumpForce = 350f * 1.5f;

	private CountdownTimer respawnTimer = new CountdownTimer(1f);
	private bool dead = false;
	public bool Dead
	{
		get => dead;
		set
		{
			if (dead == false && value == true) Die();
			dead = value;
		}
	}

	private readonly Texture attackingTexture;
	private readonly Texture normalTexture;

	public bool Attacking { get; private set; }
	public float AttackRadius { get; private set; } = 60f;
	private readonly float attackTime = 0.4f;
	private Timer attackTimer = new Timer();

	public Player(Vector2 position)
	{
		Spawnpoint = position;

		Transform.Position = position;
		Transform.Size = new Vector2(64);

		normalTexture = new Texture("./assets/player.png");
		attackingTexture = new Texture("./assets/player-attacking.png");
		Texture = normalTexture;

		HasCollisionDetection = true;
		HasCollisionResolution = true;
		HasGravity = true;
		GravityMultiplier = 2f;
	}

	public override void Update()
	{
		if (Dead == false)
		{
			Move();
			Attack();
		}

		// Respawn
		// TODO: Put this in a method
		if (Dead && respawnTimer.IsFinished)
		{
			// Dead = false;
			// ShouldDraw = true;
			// Transform.Position = Spawnpoint;
			SceneManager.ResetScene();
		}

		// Make the camera track us
		// TODO: Put this in Move()
		SceneManager.Scene.Camera.Target = Transform.WorldPosition;
	}

	private void Move()
	{
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
		bool footCollision = IsBeingCollidedWith && DirectionOfThingsBeingCollidedWith.FirstOrDefault() == Direction.Bottom;
		if ((Raylib.IsKeyDown(KeyboardKey.Space) || Raylib.IsKeyDown(KeyboardKey.Up)) && footCollision)
		{
			Velocity.Y = -jumpForce;
		}
	}

	private void Attack()
	{
		// Check for if we want to begin attacking
		if (Attacking == false && Raylib.IsKeyPressed(KeyboardKey.E))
		{
			Attacking = true;
			Texture = attackingTexture;
			attackTimer.Start();
		}

		// Check for if we've gotta end attacking
		if (Attacking == true && attackTimer.HasBeen(attackTime))
		{
			Attacking = false;
			Texture = normalTexture;
		}
	}

	public override void RenderDebugUi()
	{
		Raylib.DrawText($"L: {Transform.Position:f2}\nW: {Transform.WorldPosition:f2}\n{Velocity:f2}\n\n{ThingsBeingCollidedWith.Count}\n{DirectionOfThingsBeingCollidedWith.FirstOrDefault()}", 10, 10, 30, Color.White);
	}

    public override void RenderDebug()
    {
		if (Attacking) Raylib.DrawCircleV(Transform.CenterPosition, AttackRadius, new Color(255, 0, 0, 128));
    }

	public bool ISAttackingAndWithinAttackRadius(Transform transform)
	{
		if (Attacking == false) return false;

		return Raylib.CheckCollisionCircleRec(Transform.CenterPosition, AttackRadius, transform.Hitbox);
	}

	public void Die()
	{
		// Can't die twice
		if (dead == true) return;
		dead = true;

		SoundEffectManager.PlayDieSound();
		ShouldDraw = false;
		Velocity.X = 0f;
		GameManager.Keys = 0;

		// Start the timer then respawn after it goes off
		respawnTimer.Reset();
	}
}