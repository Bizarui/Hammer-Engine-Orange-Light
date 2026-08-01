using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;

public class Game : GameWindow
{
    private Shader shader;
    private AFG2D _figure;
    private camera2D _camera;
    private ScreenSystem2D _screen;

    private Matrix4 view;
    private Matrix4 projection;
    private Matrix4 model;

    private double _time;

    public Game(int width, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            Size = (width, height),

            Title = title
        })
    {
    }


    protected override void OnLoad()
    {
        base.OnLoad();

        shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        GL.ClearColor(0.3f, 0.4f, 0.3f, 1.0f);
        GL.Disable(EnableCap.DepthTest);

        _figure = new AFG2D(
            new Vector3(100.0f, 100.0f, 0.0f),
            new Vector3(100.0f, -100.0f, 0.0f),
            new Vector3(-100.0f, -100.0f, 0.0f),
            new Vector3(-100.0f, 100.0f, 0.0f)
        );
        _camera = new camera2D();
        _screen = new ScreenSystem2D(Size.X, Size.Y);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        _camera.Control(KeyboardState, IsFocused, (float)e.Time);
        _camera.Zoom(MouseState);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();
        
        _time += 100.0 * e.Time;
        
        Vector2 center = _screen.GetCenter();
        model = Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(_time))
              * Matrix4.CreateTranslation(center.X, center.Y, 0.0f);

        shader.SetMatrix4("model", model);
        shader.SetMatrix4("view", _camera.GetViewMatrix());
        shader.SetMatrix4("projection", _screen.GetProjectionMatrix());
        
        _figure.Draw();

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);

        if (_screen != null)
        {
            _screen.UpdateSize(Size.X, Size.Y);
        }
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _figure.Destroy();

        shader.Dispose();
    }
}
