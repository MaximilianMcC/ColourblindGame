using System.Numerics;

class KeyBox : Box
{
	public KeyBox(Vector2 position) : base(position) { }

	protected override void WhenAttacked()
	{
		Console.WriteLine("keys += 1");
	}
}