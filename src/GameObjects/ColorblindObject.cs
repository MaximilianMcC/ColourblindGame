class ColorblindObject : GameObject
{
	protected Texture NormalTexture;
	protected Texture ColorblindTexture;
	protected Texture AccessibleTexture;

	protected void AssignTextures(string normalPath, string colorblindPath, string accessiblePath)
	{
		NormalTexture = new Texture(normalPath);
		ColorblindTexture = new Texture(colorblindPath);
		AccessibleTexture = new Texture(accessiblePath);
	}

	public override void Render()
	{
		Texture texture = GameManager.VisionType switch
		{
			VisionType.Normal => NormalTexture,
			VisionType.Colorblind => ColorblindTexture,
			VisionType.Accessible => AccessibleTexture,
			_ => Texture
		};

		texture.Draw(Transform);
	}
}