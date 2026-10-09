using Raylib_cs;

class GameManager : GameObject
{
	private static int keys = 0;
	public static int Keys
	{
		get => keys;
		set
		{
			keys = value;
			if (keys >= MaxKeys) NextColorblindLevel();
		}
	}

	public static int MaxKeys { get; set; } = 3;
	public static bool HasAllKeys => Keys >= MaxKeys;

	public static VisionType VisionType;

	public override void Update()
	{
		//! debug
		if (Raylib.IsKeyPressed(KeyboardKey.C))
		{
			NextColorblindLevel();
		}
	}

	public static void NextColorblindLevel()
	{
		Keys = 0;
		SoundEffectManager.PlayColorChangeSound();
		
		switch (VisionType)
		{
			case VisionType.Normal:
				VisionType = VisionType.Colorblind;
				SceneManager.ResetScene();
				break;

			case VisionType.Colorblind:
				VisionType = VisionType.Accessible;
				SceneManager.ResetScene();
				break;
			
			case VisionType.Accessible:
				VisionType = VisionType.Normal;
				EndOfGame();
				break;
		}
	}

	public static void EndOfGame()
	{
		SceneManager.SetScene(new End());
	}

	public override void RenderUi()
	{
		Color color = VisionType == VisionType.Colorblind ? Color.RayWhite : Color.DarkGray;
		Raylib.DrawText($"Keys: {Keys}/{MaxKeys}", 23, 23, 32, color);
	}
}

public enum VisionType
{
	Normal,
	Colorblind,
	Accessible
}