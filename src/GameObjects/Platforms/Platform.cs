using System.Numerics;
using Raylib_cs;

class Platform : ColorblindObject
{
	public Platform(Vector2 position)
	{
		AssignTextures(
			"./assets/platform-normal.png",
			"./assets/platform-colorblind.png",
			"./assets/platform-accessible.png"
		);

		Transform.Position = position;
		Texture = new Texture("./assets/platform1.png");
		// Hitbox.Size = Texture.Dimensions;
		Transform.Size = new Vector2(Texture.Width * 5, Texture.Height);		
	}
}