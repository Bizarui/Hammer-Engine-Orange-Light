using OpenTK.Mathematics;

public class Transform : Component
{
    public Vector2 Position { get; set; } = Vector2.One * 100f;
    public float RotationZ { get; set; } = 0.0f;
    public Vector2 Scale { get; set; } = Vector2.One;

    public Matrix4 GetModelMatrix()
    {
        Matrix4 scaleMatrix = Matrix4.CreateScale(Scale.X, Scale.Y, 1.0f);
        Matrix4 rotationMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(RotationZ));
        Matrix4 translationMatrix = Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);

        return scaleMatrix * rotationMatrix * translationMatrix;
    }
}
