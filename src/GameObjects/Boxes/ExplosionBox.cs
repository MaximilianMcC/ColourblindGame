using System.Numerics;

class ExplosionBox : Box
{
	public ExplosionBox(Vector2 position) : base(position) { }

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