using System.Numerics;

class KeyBox : Box
{
	public KeyBox(Vector2 position) : base(position)
	{
		AssignTextures(
			"./assets/key-box-normal.png",
			"./assets/key-box-colorblind.png",
			"./assets/key-box-accessible.png"
		);
	}

	protected override void WhenAttacked()
	{
		GameManager.Keys++;
		SoundEffectManager.PlayKeySound();
		Destroy();
	}
}