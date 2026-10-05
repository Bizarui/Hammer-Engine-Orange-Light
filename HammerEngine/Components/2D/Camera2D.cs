using HammerEngine.Systems.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Camera2D : Component
{
    public Vector2 Position = new Vector2(0.0f, 0.0f);

    public float Speed = 400.0f; // пикселей в секунду
    public float ZoomScale = 1.0f;

    public Matrix4 GetViewMatrix()
    {
        return Matrix4.CreateTranslation(-Position.X, -Position.Y, 0.0f)
             * Matrix4.CreateScale(ZoomScale, ZoomScale, 1.0f);
    }

    public override void Update()
    {
        if (Input.Keyboard.IsKeyDown(Keys.A))
        {
            Position.X -= Speed * Time.deltaTime; // влево
        }
        if (Input.Keyboard.IsKeyDown(Keys.D))
        {
            Position.X += Speed * Time.deltaTime; // вправо
        }
        if (Input.Keyboard.IsKeyDown(Keys.W))
        {
            Position.Y += Speed * Time.deltaTime; // вверх
        }
        if (Input.Keyboard.IsKeyDown(Keys.S))
        {
            Position.Y -= Speed * Time.deltaTime; // вниз
        }

        Zoom(Input.Mouse);
    }

    private void Zoom(MouseState mouse)
    {
        float scroll = mouse.ScrollDelta.Y;

        if (scroll > 0.0f)
        {
            ZoomScale += 0.1f;
        }
        else if (scroll < 0.0f)
        {
            ZoomScale -= 0.1f;
        }

        if (ZoomScale < 0.1f) ZoomScale = 0.1f;
    }
}
