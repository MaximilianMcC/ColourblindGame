using System.Numerics;

class BouncyBox : Box
{
	public BouncyBox(Vector2 position) : base(position) { }

	public override void Update()
	{
		base.Update();

		Player player = SceneManager.Scene.Player;

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