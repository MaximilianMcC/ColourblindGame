using System.Numerics;
using Raylib_cs;

class DeathPlane : GameObject
{
	public float DeathHeight = 300f;

	public override void Update()
	{
		// Kill the player if they fall below the world
		if (SceneManager.Scene.Player.Transform.Position.Y > DeathHeight)
		{
			SceneManager.Scene.Player.Die();
		}
	}

	public override void RenderDebug()
	{
		const float width = 10000;
		Raylib.DrawLineEx(new Vector2(-width, DeathHeight), new Vector2(width, DeathHeight), 5f, Color.Magenta);
	}
}