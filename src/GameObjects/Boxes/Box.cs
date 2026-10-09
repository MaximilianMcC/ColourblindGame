using System.Numerics;
using Raylib_cs;

class Box : ColorblindObject
{
	public Box(Vector2 position)
	{
		AssignTextures(
			"./assets/box-normal.png",
			"./assets/box-colorblind.png",
			"./assets/box-normal.png"
		);

		Transform.Position = position;
		Transform.Size = new Vector2(64);

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

		// Check for if we're being touched
		if (ThingColliding(player, out _)) WhenTouched();
	}

	protected virtual void WhenAttacked() { }
	protected virtual void WhenTouched() { }
}