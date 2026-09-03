using Raylib_cs;

class CountdownTimer
{
	public double Duration { get; private set; }
	public double StartTime { get; private set; }
	public double EndTime { get; private set; }

	public bool Loops { get; private set; }
	public int CurrentLoop { get; private set; } = -1;

	public float Time => (float)(Raylib.GetTime() - StartTime);

	public bool IsFinished {
		get
		{
			// Check for if we're done
			bool done = Time >= Duration;

			// If we've finished and need to loop then loop
			if (done && Loops)
			{
				StartTime = Raylib.GetTime();
				CurrentLoop++;
			}

			// Say if we're done or not
			return done;
		}
	}

	public CountdownTimer(float timeInSeconds, bool loop = false)
	{
		Duration = timeInSeconds;

		Loops = loop;
		if (Loops) CurrentLoop = 0;

		// Reset sets the start time
		Reset();
		EndTime = StartTime + Duration;
	}

	public void Reset() => StartTime = Raylib.GetTime();

	public override string ToString() => $"{Time}s";
}

class Timer
{
	private double StartTime = -1d;

	public void Start() => Restart();
	public void Restart() => StartTime = Raylib.GetTime();

	public float Time => (float)(Raylib.GetTime() - StartTime);
	public bool HasBeen(float seconds) => Time >= seconds;

	public bool RestartIfHasBeen(float seconds)
	{
		bool ended = HasBeen(seconds);
		if (ended || StartTime == -1d) Restart();
		return ended;
	}

	public override string ToString() => $"{Time}s";
}