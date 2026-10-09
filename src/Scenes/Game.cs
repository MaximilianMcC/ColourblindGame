using System.Numerics;
using Raylib_cs;

class Game : Scene
{
	public override void Init()
	{
		Camera = new Camera2D(Program.GameSize / 2f, Raylib.GetScreenCenter(), 0f, 1f);

		Player = new Player(new Vector2(0, -100));
		GameObjects.Add(Player);

		GameObjects.Add(new GameManager());
		GameObjects.Add(new DeathPlane());



		LevelBuilder level = new LevelBuilder(this);	
		level.AddGap(-100f);
		level.AddPlatform(300f);
		level.AddGap(100f);
		level.AddPlatform(300f);
		level.AddGap(100f);
		level.MoveUp();
		level.AddPlatform(100f);
		level.AddGap(100f);
		level.MoveUp();
		level.AddPlatform(100f);
		level.AddGap(100f);
		level.MoveUp();
		level.AddPlatform(500f);
		level.AddRandomBox(-300f);
		level.AddGap(200f);
		level.AddPlatform(500f);
		level.AddRandomBox(-300f);
		level.MoveUp();
		level.AddGap(100f);
		level.AddPlatform(300f);
		level.AddBouncyBox(-64f);
		level.MoveUp(64f * 5);
		level.AddPlatform(500f);
		level.AddRandomBox(-350f);
		level.AddGap(150f);
		level.AddPlatform(300f);
		level.AddGap(100f);
		level.MoveUp();
		level.AddPlatform(200f);
		level.AddRandomBox(-100f);
		level.AddGap(150f);
		level.MoveUp();
		level.AddPlatform(400f);
		level.AddRandomBox(-250f);
		level.AddGap(200f);
		level.AddPlatform(300f);
		level.AddExplosionBox(-150f);
		level.MoveUp();
		level.AddGap(100f);
		level.AddPlatform(400f);
		level.AddRandomBox(-200f);
		level.AddGap(150f);
		level.AddPlatform(500f);
		level.AddRandomBox(-350f);
		level.AddGap(200f);
		level.AddPlatform(300f);
		level.AddBouncyBox(-64f);
		level.MoveUp(64 * 3);
		level.AddGap(100f);
		level.AddPlatform(500f);
		level.AddRandomBox(-300f);
		level.InsertKeyBoxes();
	}
}