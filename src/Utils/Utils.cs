using System.Numerics;

static class Utils
{
	public static int BoolI(bool boolean) => boolean ? 1 : 0;
	public static float BoolF(bool boolean) => boolean ? 1f : 0f;

	public static Direction GetDirectionOfThing(Transform me, Transform thing)
	{
		Direction direction = Direction.Unknown;
		
		// Extract all of 'my' corners
		// TODO: Don't do this
		float aLeft = me.WorldPosition.X;
		float aRight = me.WorldPosition.X + me.Width;
		float aTop = me.WorldPosition.Y;
		float aBottom = me.WorldPosition.Y + me.Height;

		// Extract all of the other corners
		// TODO: Don't do this
		float bLeft = thing.WorldPosition.X;
		float bRight = thing.WorldPosition.X + thing.Width;
		float bTop = thing.WorldPosition.Y;
		float bBottom = thing.WorldPosition.Y + thing.Height;

		// Get how far we are on the x or y
		float overlapX = MathF.Min(aRight, bRight) - MathF.Max(aLeft, bLeft);
		float overlapY = MathF.Min(aBottom, bBottom) - MathF.Max(aTop, bTop);
		
		// Check for where we are
		if (overlapX < overlapY)
		{
			// Horizontal
			direction = (me.CenterPosition.X > thing.CenterPosition.X) ? Direction.Left : Direction.Right;
		}
		else
		{
			// Vertical
			direction = (me.CenterPosition.Y > thing.CenterPosition.Y) ? Direction.Top : Direction.Bottom;
		}

		return direction;
	}
}

enum Direction
{
	Unknown,
	Top,
	Bottom,
	Left,
	Right
}