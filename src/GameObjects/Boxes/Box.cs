using System.Numerics;
using Raylib_cs;

class Box : GameObject
{
	public Box(Vector2 position)
	{
		Transform.Position = position;
		Transform.Size = new Vector2(64);

		Texture = new Texture("./assets/box1.png");

		HasCollisionDetection = true;
	}

	public override void Update()
	{
		Player player = SceneManager.Scene.Player;

		// Check for if we're attacked
		if (player.ISAttackingAndWithinAttackRadius(Transform))
		{
			WhenAttacked();
		}
	}

	protected virtual void WhenAttacked() { }
}