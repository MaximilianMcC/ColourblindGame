using Raylib_cs;

class SoundEffectManager
{
	private static Sound colorChange;
	private static Sound die;
	private static Sound key;

	public static void LoadAllSounds()
	{
		colorChange = AssetManager.LoadSound("./assets/color-change.wav");
		die = AssetManager.LoadSound("./assets/die.wav");
		key = AssetManager.LoadSound("./assets/key.wav");
	}

	public static void PlayColorChangeSound() => Raylib.PlaySound(colorChange);
	public static void PlayDieSound() => Raylib.PlaySound(die);
	public static void PlayKeySound() => Raylib.PlaySound(key);

	public static void UnloadAllSounds()
	{
		Raylib.UnloadSound(colorChange);
		Raylib.UnloadSound(die);
		Raylib.UnloadSound(key);
	}
}