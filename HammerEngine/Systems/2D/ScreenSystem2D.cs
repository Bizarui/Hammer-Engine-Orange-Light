using OpenTK.Mathematics;

public class ScreenSystem2D
{
    public float Width { get; private set; }
    public float Height { get; private set; }

    public ScreenSystem2D(int startWidth, int startHeight)
    {
        UpdateSize(startWidth, startHeight);
    }
    public void UpdateSize(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public Matrix4 GetProjectionMatrix()
    {
        return Matrix4.CreateOrthographicOffCenter(0.0f, Width, 0.0f, Height, -1.0f, 1.0f);
    }

    public Vector2 GetCenter()
    {
        return new Vector2(Width / 2.0f, Height / 2.0f);
    }
}