using System.Numerics;
using Raylib_cs;

class GameObject
{
	public Vector2 Position;
	public Rectangle Hitbox;

	public Texture2D Texture;

	public bool HasCollision = true;
	public bool IsBeingCollidedWith { get; private set; }
	public GameObject ThingBeingCollidedWith { get; private set; }

	public virtual void Update() { }

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
		ThingBeingCollidedWith = null;

		foreach (GameObject thing in SceneManager.Scene.GameObjects.Where(x => x.HasCollision && x != this))
		{
			// Check for collision
			if (Raylib.CheckCollisionRecs(thing.Hitbox, Hitbox))
			{
				// Update our stuff
				IsBeingCollidedWith = true;
				ThingBeingCollidedWith = thing;

				// Update the victims stuff
				thing.IsBeingCollidedWith = true;
				thing.ThingBeingCollidedWith = this;
			}
		}
	}
}