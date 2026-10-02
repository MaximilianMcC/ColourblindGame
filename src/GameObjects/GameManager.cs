using Raylib_cs;

class GameManager : GameObject
{
	public static int Keys { get; set; } = 0;
	public static int MaxKeys { get; set; } = 3;
	public static bool HasAllKeys => Keys >= MaxKeys;

	public static VisionType VisionType;

	public override void Update()
	{
		//! debug
		if (Raylib.IsKeyPressed(KeyboardKey.C))
		{
			NextLevel();
		}
	}

	public static void NextLevel()
	{
		Keys = 0;
		SoundEffectManager.PlayColorChangeSound();
		
		switch (VisionType)
		{
			case VisionType.Normal:
				VisionType = VisionType.Colorblind;
				break;

			case VisionType.Colorblind:
				VisionType = VisionType.Accessible;
				break;
			
			case VisionType.Accessible:
				EndOfGame();
				break;
		}
	}

	public static void EndOfGame()
	{
		Console.WriteLine("Game end");
	}

	public override void RenderUi()
	{
		Raylib.DrawText($"Keys: {Keys}/{MaxKeys}", 23, 23, 32, Color.Gold);
	}
}

public enum VisionType
{
	Normal,
	Colorblind,
	Accessible
}