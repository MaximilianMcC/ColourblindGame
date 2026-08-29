using System.Numerics;
using Raylib_cs;

class Platform : GameObject
{
	public Platform(Vector2 position)
	{
		Position = position;
		Texture = Raylib.LoadTexture("./assets/platform1.png");
		Hitbox.Size = Texture.Dimensions;
	}
}