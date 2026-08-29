using System.Numerics;
using Raylib_cs;

class Transform
{
	public Rectangle Hitbox => new Rectangle(WorldPosition, Size);
	public Vector2 CenterPosition => Hitbox.Center;

	public Vector2 Position;
	public Vector2 Size;

	public Transform Parent;

	public Vector2 WorldPosition
	{
		get
		{
			if (Parent == null) return Position;
			return Parent.WorldPosition + Position;
		}	
	}

	public void UnbindParent()
	{
		// Make sure we actually have a parent
		if (Parent == null) return;

		// Make our local position our world position (without the parent)
		Vector2 worldPosition = WorldPosition;
		Parent = null;
		Position = worldPosition;
	}
}