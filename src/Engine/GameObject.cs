using System.Numerics;
using Raylib_cs;

class GameObject
{
	public Transform Transform = new Transform();

	public Vector2 Velocity;
	public float GravityMultiplier = 1f;

	// If we disable gravity then reset Y velocity too
	private bool hasGravity;
	public bool HasGravity {
		get => hasGravity;
		set {
			hasGravity = value;
			if (value == false) Velocity.Y = 0f;
		}
	}

	public Texture2D Texture;

	public bool HasCollisionDetection = true;
	public bool HasCollisionResolution = false;
	
	// TODO: Make these lists private then with a public readonly getter
	public bool IsBeingCollidedWith { get; private set; }
	public List<GameObject> ThingsBeingCollidedWith = [];
	public List<Direction> DirectionOfThingsBeingCollidedWith = [];

	public virtual void Update() { }

	public virtual void RenderUi() { }
	public virtual void RenderDebugUi() { }
	public virtual void RenderDebug() { }
	public virtual void Render()
	{
		Raylib.DrawTexturePro(
			Texture,
			new Rectangle(0, 0, Texture.Dimensions),
			Transform.Hitbox,
			Vector2.Zero,
			0f,
			Color.White
		);

		if (Program.DebugMode)
		{
			// Draw our hitbox
			Raylib.DrawRectangleLinesEx(Transform.Hitbox, 3f, IsBeingCollidedWith ? Color.Magenta : Color.Green);

			// Draw a line to the positional parent if we have one
			// TODO: Make it go from the centre
			if (Transform.Parent != null) Raylib.DrawLineEx(Transform.WorldPosition, Transform.Parent.WorldPosition, 2f, Color.White);
		}
	}

	public virtual void CleanUp()
	{
		Raylib.UnloadTexture(Texture);
	}

	public void CheckForCollision()
	{
		// Reset our stats
		IsBeingCollidedWith = false;
		ThingsBeingCollidedWith.Clear();
		DirectionOfThingsBeingCollidedWith.Clear();

		foreach (GameObject thing in SceneManager.Scene.GameObjects)
		{
			// Don't collide with ourself
			if (thing == this) continue;

			// Don't collide with stuff if they haven't got collision
			if (thing.HasCollisionDetection == false) continue;
			
			// Check for collision
			if (Raylib.CheckCollisionRecs(thing.Transform.Hitbox, Transform.Hitbox))
			{
				// Say we're being collided with
				IsBeingCollidedWith = true;
				ThingsBeingCollidedWith.Add(thing);
				DirectionOfThingsBeingCollidedWith.Add(Utils.GetDirectionOfThing(Transform, thing.Transform));
			}
		}
	}

	public void ResolveCollisions()
	{
		// Check for if we even can do this
		if (HasCollisionResolution == false) return;

		// Loop over everything being collided with
		foreach (GameObject thing in ThingsBeingCollidedWith)
		{
			ResolveCollision(thing);
		}
	}

	public void ResolveCollision(GameObject victim)
	{
		// Get the centre of the two objects
		Vector2 ourCenter = Transform.CenterPosition;
		Vector2 victimsCenter = victim.Transform.CenterPosition;

		// Get the X and Y overlap based on how far apart they are
		Vector2 distance = ourCenter - victimsCenter;
		Vector2 halfSize = (Transform.Hitbox.Size + victim.Transform.Hitbox.Size) / 2f;
		Vector2 overlap = halfSize - Vector2.Abs(distance);

		// Resolve collision
		// TODO: Use hitbox position
		if (overlap.X < overlap.Y)
		{
			// Check for if we're going left/right
			if (distance.X > 0) Transform.Position.X += overlap.X;
			else Transform.Position.X -= overlap.X;

			// Reset velocity
			Velocity.X = 0f;
		}
		else
		{
			// Check for if we're going up/down
			if (distance.Y > 0) Transform.Position.Y += overlap.Y;
			else Transform.Position.Y -= overlap.Y;

			// Reset velocity
			Velocity.Y = 0f;
		}
	}

	protected void ActAsPositionalParentForThingsCollidingWithUs()
	{
		// If something is colliding with us then act as their positional parent
		if (IsBeingCollidedWith == false) return;

		foreach (GameObject child in ThingsBeingCollidedWith)
		{
			SetPositionalChild(child);
		}
	}

	public void SetPositionalChild(GameObject child)
	{
		// Check for if we are already the parent of this thing
		if (child.Transform.Parent == Transform) return;

		// Update the child's position to be related to us
		Vector2 childWorldPosition = child.Transform.WorldPosition;
		child.Transform.Parent = Transform;
		child.Transform.Position = childWorldPosition - Transform.WorldPosition;	
	}
}