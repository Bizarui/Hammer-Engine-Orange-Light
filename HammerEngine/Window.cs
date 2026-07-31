using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;

public class Game : GameWindow
{
    private Shader shader;
    private AFG2D mySquare;

    private Matrix4 view;
    private Matrix4 projection;
    private Matrix4 model;

    private double _time;

    public Game(int width, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            Size = (width, height),
            MaximumSize = (width, height),
            MinimumSize = (width, height),
            Title = title
        })
    {
    }


    protected override void OnLoad()
    {
        base.OnLoad();

        shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        GL.ClearColor(0.3f, 0.4f, 0.3f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        projection = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45.0f),
            (float)Size.X / Size.Y,
            0.1f,
            100.0f
        );
        model = Matrix4.Identity;

        mySquare = new AFG2D(
            new Vector3(0.5f, 0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f, 0.5f, 0.0f)
        );

    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        _time += 100.0 * e.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Use();

        model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));

        shader.SetMatrix4("model", model);
        shader.SetMatrix4("view", view);
        shader.SetMatrix4("projection", projection);

        mySquare.Draw();

        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        mySquare.Destroy();

        shader.Dispose();
    }
}
