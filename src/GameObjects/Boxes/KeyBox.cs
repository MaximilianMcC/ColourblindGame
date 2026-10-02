using System.Numerics;

class KeyBox : Box
{
	public KeyBox(Vector2 position) : base(position)
	{
		Texture = new Texture("./assets/key.png");
		Transform.Size = new Vector2(Texture.Width, Texture.Height);
	}

	protected override void WhenAttacked()
	{
		GameManager.Keys++;
		SoundEffectManager.PlayKeySound();
	}
}