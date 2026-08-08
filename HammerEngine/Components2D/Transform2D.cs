using OpenTK.Mathematics;

public class Transform2D : Component
{
    public Vector2 Position { get; set; } = new Vector2(100.0f, 100.0f);
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Scale { get; set; } = new Vector2(1.0f, 1.0f);

    public Matrix4 GetModelMatrix()
    {
        Matrix4 scaleMatrix = Matrix4.CreateScale(Scale.X, Scale.Y, 1.0f);
        Matrix4 rotationMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation));
        Matrix4 translationMatrix = Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);

        return scaleMatrix * rotationMatrix * translationMatrix;
    }
}
