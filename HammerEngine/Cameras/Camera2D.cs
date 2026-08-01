using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class camera2D
{
    public Vector2 Position = new Vector2(0.0f, 0.0f);

    public float Speed = 400.0f; // пикселей в секунду

    public Matrix4 GetViewMatrix()
    {
        return Matrix4.CreateTranslation(-Position.X, -Position.Y, 0.0f);
    }

    public void Control(KeyboardState input, bool isFocused, float deltaTime)
    {
        if (!isFocused) return;

        if (input.IsKeyDown(Keys.A))
        {
            Position.X -= Speed * deltaTime; // влево
        }
        if (input.IsKeyDown(Keys.D))
        {
            Position.X += Speed * deltaTime; // вправо
        }
        if (input.IsKeyDown(Keys.W))
        {
            Position.Y += Speed * deltaTime; // вверх
        }
        if (input.IsKeyDown(Keys.S))
        {
            Position.Y -= Speed * deltaTime; // вниз
        }
    }
}
