using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;

public class Game : GameWindow
{
    private Shader shader;
    private AFG2D mySquare;

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

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();

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
