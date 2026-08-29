using System.Numerics;
using Raylib_cs;

class GameObject
{
	public Vector2 Position;
	public Rectangle Hitbox;

	public Vector2 Velocity;
	public bool HasGravity = false;

	public Texture2D Texture;

	public bool HasCollisionDetection = true;
	public bool HasCollisionResolution = false;
	
	public bool IsBeingCollidedWith { get; private set; }
	public List<GameObject> ThingsBeingCollidedWith = [];

	public virtual void Update() { }

	public virtual void RenderUi() { }
	public virtual void Render()
	{
		Raylib.DrawTexturePro(
			Texture,
			new Rectangle(0, 0, Texture.Dimensions),
			Hitbox,
			Vector2.Zero,
			0f,
			Color.White
		);

		if (Program.DebugMode)
		{
			Raylib.DrawRectangleLinesEx(Hitbox, 3f, IsBeingCollidedWith ? Color.Magenta : Color.Green);
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

		foreach (GameObject thing in SceneManager.Scene.GameObjects)
		{
			// Don't collide with ourself
			if (thing == this) continue;

			// Don't collide with stuff if they haven't got collision
			if (thing.HasCollisionDetection == false) continue;
			
			// Check for collision
			if (Raylib.CheckCollisionRecs(thing.Hitbox, Hitbox))
			{
				IsBeingCollidedWith = true;
				ThingsBeingCollidedWith.Add(thing);
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
		// TODO: Use normal position instead of centre
		Vector2 ourCenter = Hitbox.Center;
		Vector2 victimsCenter = victim.Hitbox.Center;

		// Get the X and Y overlap based on how far apart they are
		Vector2 distance = ourCenter - victimsCenter;
		Vector2 halfSize = (Hitbox.Size + victim.Hitbox.Size) / 2f;
		Vector2 overlap = halfSize - Vector2.Abs(distance);

		// Resolve collision
		// TODO: Try to do this with vectors instead of axis
		if (overlap.X < overlap.Y)
		{
			// Check for if we're going left/right
			if (distance.X > 0) Position.X += overlap.X;
			else Position.X -= overlap.X;

			// Reset velocity
			Velocity.X = 0f;
		}
		else
		{
			// Check for if we're going up/down
			if (distance.Y > 0) Position.Y += overlap.Y;
			else Position.Y -= overlap.Y;

			// Reset velocity
			Velocity.Y = 0f;
		}

		// Keep the hitbox synced
		Hitbox.Position = Position;
	}
}