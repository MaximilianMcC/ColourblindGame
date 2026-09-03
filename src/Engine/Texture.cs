using System.Numerics;
using Raylib_cs;

class Texture
{
	public bool Initialised { get; private set; }

	private bool animated = false;
	public bool IsAnimated => animated;
	public bool AnimationPaused { get; set; } = false;

	private readonly Timer frameTimer = new Timer();
	public float Fps { get; set; } = -1f;
	public float FrameWidth { get; private set; }
	public int Frames { get; private set; }
	public int CurrentFrame { get; private set; }
	
	public Rectangle Rectangle => new Rectangle(0, 0, Dimensions);
	public Vector2 Dimensions => RaylibTexture.Dimensions;
	public float Width => Dimensions.X;
	public float Height => Dimensions.Y;

	public Texture2D RaylibTexture { get; private set; }	
	private static Dictionary<string, Texture2D> loadedTextures = [];

	// Static texture texture
	public Texture(string path)
	{
		animated = false;
		RaylibTexture = LoadTextureIfItIsntAlreadyLoaded(path);
		Initialised = true;
	}

	// Animated texture
	public Texture(string path, int frameWidth, float fps)
	{
		animated = true;
		RaylibTexture = LoadTextureIfItIsntAlreadyLoaded(path);

		Fps = fps;
		FrameWidth = frameWidth;

		// Figure out how many frames we've got
		Frames = RaylibTexture.Width / frameWidth;

		Initialised = true;
	}

	private static Texture2D LoadTextureIfItIsntAlreadyLoaded(string path)
	{
		// Load the texture if its not already loaded
		if (loadedTextures.ContainsKey(path) == false)
		{
			loadedTextures[path] = Raylib.LoadTexture(path);
		}

		// Give back the texture
		return loadedTextures[path];
	}

	public void Draw(Transform transform)
	{
		// Draw the regular normal static texture
		if (IsAnimated == false)
		{
			Raylib.DrawTexturePro(
				RaylibTexture,
				Rectangle,
				transform.Hitbox,
				Vector2.Zero,
				0f,
				Color.White
			);

			return;
		}

		// Update our frame if needed
		if (AnimationPaused == false)
		{
			// TODO: Use modulo
			if (frameTimer.RestartIfHasBeen(1f / Fps)) CurrentFrame++;
			if (CurrentFrame > Frames) CurrentFrame = 0;
		}

		// Figure out the section of the texture that we'd like to draw
		Rectangle section = new Rectangle(0, Width * CurrentFrame, Dimensions);

		// Draw it
		Raylib.DrawTexturePro(
			RaylibTexture,
			section,
			transform.Hitbox,
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public void CleanUp()
	{
		// TODO: make it so that the loaded texture dictionary also contains a number of things that are using the texture so that we can only unload the texture if this is the last thing that is using the texture
		Console.WriteLine("TODO: unload texture");
	}
}