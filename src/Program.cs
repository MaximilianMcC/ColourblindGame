using System.Numerics;
using Raylib_cs;

class Program
{
	public static bool DebugMode = false;

	public static Vector2 GameSize = new Vector2(1920, 1080) * 0.7f;

	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow(1280, 720, "You're colourblind? What colour is this pencil then?");
		Raylib.InitAudioDevice();

		SoundEffectManager.LoadAllSounds();

		SceneManager.SetScene(new Start());

		RenderTexture2D renderTexture = Raylib.LoadRenderTexture((int)GameSize.X, (int)GameSize.Y);
		Vector2 previousScreenSize = Vector2.Zero;

		while (Raylib.WindowShouldClose() == false)
		{
			if (Raylib.IsKeyPressed(KeyboardKey.Grave)) DebugMode = !DebugMode;
			SceneManager.Update();

			// Check for if the screen is resized
			// Vector2 currentScreenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
			// if (currentScreenSize != previousScreenSize)
			// {
			// 	previousScreenSize = currentScreenSize;
			// 	SceneManager.Scene.Camera.Offset = Raylib.GetScreenCenter();
			// }

			// Draw the actual game
			Raylib.BeginTextureMode(renderTexture);
			Raylib.ClearBackground(Color.White);
			SceneManager.Render();
			Raylib.EndTextureMode();

			// Draw to the actual screen
			Raylib.BeginDrawing();
			Raylib.DrawTexturePro(
				renderTexture.Texture,
				new Rectangle(0, 0, renderTexture.Texture.Dimensions * new Vector2(1, -1)),
				new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()),
				Vector2.Zero,
				0f,
				Color.White
			);
			if (DebugMode) Raylib.DrawText($"DEBUG MODE ENABLED", 10, Raylib.GetScreenHeight() - 10, 8, Color.White);
			Raylib.EndDrawing();
		}

		SoundEffectManager.UnloadAllSounds();
		SceneManager.SetScene(null);
		Raylib.CloseAudioDevice();
		Texture.UnloadAll();
		Raylib.CloseWindow();
	}
}