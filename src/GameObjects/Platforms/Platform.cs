using System.Numerics;
using Raylib_cs;

class Platform : GameObject
{
	public Platform(Vector2 position)
	{
		Transform.Position = position;
		Texture = new Texture("./assets/platform1.png");
		// Hitbox.Size = Texture.Dimensions;
		Transform.Size = new Vector2(Texture.Width * 5, Texture.Height);
	}
}