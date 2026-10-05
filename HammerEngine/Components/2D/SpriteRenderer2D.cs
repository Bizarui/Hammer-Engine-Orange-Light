using OpenTK.Mathematics;

public class SpriteRenderer2D : Component
{
    public Color4 Color { get; set; } = Color4.White;

    public int TextureHandle { get; set; } = 0;

    public SpriteRenderer2D(Color4 color)
    {
        Color = color;
    }
}