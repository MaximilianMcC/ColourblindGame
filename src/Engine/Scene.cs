using System.Numerics;
using Raylib_cs;

abstract class Scene
{
	public List<GameObject> GameObjects = [];
	public Camera2D Camera;
	public Player Player;

	public readonly float Gravity = 600f;

	public abstract void Init();
}

static class SceneManager
{
	public static Scene Scene { get; private set; } = null;

	public static void SetScene(Scene scene)
	{
		// Unload the old scene if we had one
		if (Scene != null) foreach (GameObject thing in Scene.GameObjects) thing.CleanUp();

		// Make the new scene
		Scene = scene;
		scene?.Init();
	}

	public static void Update()
	{
		if (Scene == null) return;

		// Update everything
		foreach (GameObject thing in Scene.GameObjects)
		{
			// Update
			thing.Update();

			// Handle gravity and velocity
			if (thing.HasGravity) thing.Velocity.Y += (Scene.Gravity * thing.GravityMultiplier) * Raylib.GetFrameTime();
			thing.Transform.Position += thing.Velocity * Raylib.GetFrameTime();

			thing.Transform.UnbindParent();
		}

		// Check for collision
		foreach (GameObject thing in Scene.GameObjects)
		{
			thing.CheckForCollision();
		}

		// Fix collision
		foreach (GameObject thing in Scene.GameObjects)
		{
			thing.ResolveCollisions();
		}
	}

	public static void Render()
	{
		if (Scene == null)
		{
			Raylib.DrawText("No scene selected!", 0, 0, 30, Color.White);
			return;
		}

		Raylib.BeginMode2D(Scene.Camera);
		foreach (GameObject thing in Scene.GameObjects)
		{
			thing.Render();
			if (Program.DebugMode) thing.RenderDebug();
		}
		Raylib.EndMode2D();
		
		foreach (GameObject thing in Scene.GameObjects) thing.RenderUi();
		if (Program.DebugMode) foreach (GameObject thing in Scene.GameObjects) thing.RenderDebugUi();
	}
}