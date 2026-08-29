using Raylib_cs;

abstract class Scene
{
	public List<GameObject> GameObjects = [];
	public Camera2D Camera;

	public abstract void Init();
}

static class SceneManager
{
	public static Scene Scene { get; private set; } = null;

	public static void SetScene(Scene scene)
	{
		// Unload the old scene
		if (Scene != null) foreach (GameObject thing in Scene.GameObjects) thing.CleanUp();

		// Make the new scene
		Scene = scene;
		scene?.Init();
	}

	public static void Update()
	{
		if (Scene == null) return;

		foreach (GameObject thing in Scene.GameObjects)
		{
			thing.Update();
			thing.Hitbox.Position = thing.Position;
		}

		foreach (GameObject thing in Scene.GameObjects)
		{
			thing.CheckForCollision();
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
		foreach (GameObject thing in Scene.GameObjects) thing.Render();
		Raylib.EndMode2D();
	}
}