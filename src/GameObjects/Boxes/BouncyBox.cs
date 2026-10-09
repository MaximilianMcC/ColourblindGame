using System.Numerics;

class BouncyBox : Box
{
	private float bounceStrength = 850;

	public BouncyBox(Vector2 position) : base(position)
	{
		AssignTextures(
			"./assets/bouncy-box-normal.png",
			"./assets/bouncy-box-colorblind.png",
			"./assets/bouncy-box-accessible.png"
		);
	}

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
			// Make the player bounce
			//! -1 is to stop us from colliding
			// TODO: Fix
			player.Transform.Position.Y -= 1f;
			player.Velocity.Y = -bounceStrength;
		}
	}
}