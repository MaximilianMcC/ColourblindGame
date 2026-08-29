public struct NFloat
{
	private float value;
	public float Value
	{
		get => value;
		set => this.value = Math.Clamp(value, 0f, 1f);
	}

	public NFloat(float value) => this.value = Math.Clamp(value, 0f, 1f); 

	// Act as a float
	public static implicit operator NFloat(float value) => new NFloat(value);
	public static implicit operator float(NFloat value) => value.value;

	public override string ToString() => value.ToString();
}