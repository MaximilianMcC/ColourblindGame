using System.Numerics;
using Raylib_cs;

class Box : GameObject
{
	public Box(Vector2 position)
	{
		Transform.Position = position;
		Transform.Size = new Vector2(64);

		Texture = Raylib.LoadTexture("./assets/box1.png");

		HasCollisionDetection = true;
	}

	public override void Update()
	{
		Player player = SceneManager.Scene.Player;

		// Check for if we're attacked
		if (player.ISAttackingAndWithinAttackRadius(Transform))
		{
			Console.WriteLine("attacked");
		}

		// Check for if we're bounced on
		bool playerCollidingAboveUs = false;
		if (IsBeingCollidedWith)
		{
			for (int i = 0; i < ThingsBeingCollidedWith.Count; i++)
			{
				if (GetCollisionDetails(i).Direction == Direction.Top && GetCollisionDetails(i).GameObject == player)
				{
					playerCollidingAboveUs = true;
					break;
				}
			}
		}

		if (playerCollidingAboveUs)
		{
			Console.WriteLine("bounce");

			// Make the player bounce
			//! -1 is to stop us from colliding
			// TODO: Fix
			player.Transform.Position.Y -= 1f;
			player.Velocity.Y = -500f;
		}
	}
}