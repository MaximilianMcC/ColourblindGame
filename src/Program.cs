using System.Numerics;
using Raylib_cs;

class Program
{
	public static bool DebugMode = false;

	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow(720, 512, "You're colourblind? What colour is this pencil?");

		SceneManager.SetScene(new Game());

		RenderTexture2D renderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

		while (Raylib.WindowShouldClose() == false)
		{
			if (Raylib.IsKeyPressed(KeyboardKey.Grave)) DebugMode = !DebugMode;
			SceneManager.Update();

			// Draw the actual game
			Raylib.BeginTextureMode(renderTexture);
			Raylib.ClearBackground(Color.Black);
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

		SceneManager.SetScene(null);
		Raylib.CloseWindow();
	}
}