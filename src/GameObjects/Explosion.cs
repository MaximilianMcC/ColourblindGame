using System.Numerics;
using Raylib_cs;

class Explosion : GameObject
{
	public float BlastRadius;

	public Explosion(Vector2 centerPosition, float blastRadius = 100f)
	{
		Transform.Size = new Vector2(blastRadius);
		Transform.Position = centerPosition - (Transform.Size / 2f);

		Texture = new Texture("./assets/explosion.png", 64, 8f);
		BlastRadius = blastRadius;

		HasCollisionDetection = false;
	}

	public override void Update()
	{
		// Check for if the player is within the blast radius
		Player player = SceneManager.Scene.Player;
		if (Raylib.CheckCollisionCircleRec(Transform.Position, BlastRadius, player.Transform.Hitbox))
		{
			Console.WriteLine("player dead");
		}

		// Play the explosion once then remove ourselves from the scene
		if (Texture.AnimationFinishedThisFrame) Destroy();
	}

	public override void RenderDebug()
	{
		Raylib.DrawCircleV(Transform.CenterPosition, BlastRadius, new Color(255, 0, 0, 128));
	}
}