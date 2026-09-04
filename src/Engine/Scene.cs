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

		// Remove anything that needs to be removed
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			if (Scene.GameObjects[i].QueuedForDeletion) Scene.GameObjects.Remove(Scene.GameObjects[i]);
		}

		// Update everything
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			GameObject thing = Scene.GameObjects[i];

			// Update
			thing.Update();

			// Handle gravity and velocity
			if (thing.HasGravity) thing.Velocity.Y += (Scene.Gravity * thing.GravityMultiplier) * Raylib.GetFrameTime();
			thing.Transform.Position += thing.Velocity * Raylib.GetFrameTime();

			thing.Transform.UnbindParent();
		}

		// Check for collision
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			Scene.GameObjects[i].CheckForCollision();
		}

		// Fix collision
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			Scene.GameObjects[i].ResolveCollisions();
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
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			GameObject thing = Scene.GameObjects[i];
			
			thing.Render();
			if (Program.DebugMode) thing.RenderDebug();
		}
		Raylib.EndMode2D();
		
		for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			Scene.GameObjects[i].RenderUi();
		}

		if (Program.DebugMode)
		{
			for (int i = Scene.GameObjects.Count - 1; i >= 0 ; i--)
			{
				Scene.GameObjects[i].RenderDebugUi();
			}
		}
	}
}