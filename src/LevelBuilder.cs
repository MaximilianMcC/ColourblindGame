using System.Numerics;
using System.Runtime.InteropServices;

class LevelBuilder
{
	public Vector2 Cursor;

	public Scene Scene;
	public LevelBuilder(Scene scene)
	{
		Scene = scene;
	}

	public void MoveUp(float amount = 64f)
	{
		Cursor.Y -= amount;
	}

	public void AddGap(float width = 64f)
	{
		Cursor.X += width;
	}

	public void AddPlatform(float width)
	{
		Scene.GameObjects.Add(new Platform(Cursor, width));
		Cursor += new Vector2(width, 0);
	}

	public void AddBox(float xOffset)
	{
		Scene.GameObjects.Add(new Box(Cursor + new Vector2(xOffset, -64)));
	}

	public void AddExplosionBox(float xOffset)
	{
		Scene.GameObjects.Add(new ExplosionBox(Cursor + new Vector2(xOffset, -64)));
	}

	public void AddBouncyBox(float xOffset)
	{
		Scene.GameObjects.Add(new BouncyBox(Cursor + new Vector2(xOffset, -64)));
	}

	public void AddKeyBox(float xOffset)
	{
		Scene.GameObjects.Add(new KeyBox(Cursor + new Vector2(xOffset, -64)));
	}

	public void AddRandomBox(float xOffset, bool includeBouncy = false)
	{
		switch (Random.Shared.Next(1, 3 + (includeBouncy ? 1 : 0)))
		{
			case 1:
				AddBox(xOffset);
				break;

			case 2:
				AddExplosionBox(xOffset);
				break; 

			case 3:
				AddBouncyBox(xOffset);
				break;			
		}
	}

	public void InsertKeyBoxes()
	{
		// Get all boxes in the level and shuffle them around
		List<Box> boxes = Scene.GameObjects.OfType<Box>().Where(x => x is not BouncyBox).ToList();
		Random.Shared.Shuffle(CollectionsMarshal.AsSpan(boxes));

		// Select x to turn into key boxes
		for (int i = 0; i < GameManager.MaxKeys; i++)
		{
			// Make a key box in its place
			Scene.GameObjects.Add(new KeyBox(boxes[i].Transform.Position));

			// Remove/'replace' the old box
			boxes[i].Destroy();
		}
	}
}