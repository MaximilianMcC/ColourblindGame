using Raylib_cs;

class End : Scene
{
	public override void Init()
	{
		GameObjects.Add(new EndStuff());
	}
}

class EndStuff : GameObject
{
	public override void Update()
	{
		if (Raylib.IsKeyPressed(KeyboardKey.Space)) SceneManager.SetScene(new Game());
	}

	public override void RenderUi()
	{
		Utils.DrawMiddleText("You have finished all three vision types!\n\nPress space to restart", 32f);
	}
}