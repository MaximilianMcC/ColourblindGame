using Raylib_cs;

class Start : Scene
{
	public override void Init()
	{
		GameObjects.Add(new StartStuff());
	}
}

class StartStuff : GameObject
{
	public override void Update()
	{
		if (Raylib.IsKeyPressed(KeyboardKey.Space)) SceneManager.SetScene(new Game());
	}

	public override void RenderUi()
	{
		Utils.DrawMiddleText("After you have collected all three keys you will\nmove onto the next vision setting:\nNormal vision, colourblind vision, accessibility vision\n\nArrow keys to move, R to rest, E to attack.\n\nPress space to begin", 32f);
	}
}