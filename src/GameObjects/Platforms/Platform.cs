using System.Numerics;
using Raylib_cs;

class Platform : ColorblindObject
{
	public Platform(Vector2 position, float width = -1f)
	{
		AssignTextures(
			"./assets/platform-normal.png",
			"./assets/platform-colorblind.png",
			"./assets/platform-accessible.png"
		);

		Transform.Position = position;
		Texture = new Texture("./assets/platform1.png");
		Transform.Size = new Vector2(width == -1f ? Texture.Width : width, Texture.Height);
	}
}