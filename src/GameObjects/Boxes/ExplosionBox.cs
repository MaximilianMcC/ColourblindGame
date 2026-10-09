using System.Numerics;

class ExplosionBox : Box
{
	public ExplosionBox(Vector2 position) : base(position)
	{
		AssignTextures(
			"./assets/explosion-box-normal.png",
			"./assets/explosion-box-colorblind.png",
			"./assets/explosion-box-accessible.png"
		);
		HasCollisionDetection = false;
	}

	protected override void WhenAttacked() => Explode();
	protected override void WhenTouched() => Explode();

	public void Explode()
	{
		// Make an explosion
		SceneManager.Scene.GameObjects.Add(new Explosion(Transform.CenterPosition));

		// Remove ourself
		Destroy();
	}
}