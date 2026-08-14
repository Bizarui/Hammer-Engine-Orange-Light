using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using HammerEngine;

public class Game : GameWindow
{
    private Scene _mainScene;

    private ScreenSystem2D _screen;
    private RenderSystem2D _renderSystem;

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

        _renderSystem = new RenderSystem2D("Shaders/shader.vert", "Shaders/shader.frag");

        GL.ClearColor(0.3f, 0.4f, 0.3f, 1.0f);
        GL.Disable(EnableCap.DepthTest);

        _mainScene = SceneManager.CreateScene();
        
        SceneManager.LoadScene(0);

        GameObject Object1 = new GameObject();
        Object1.Name = "Triangle";

        Object1.transform.Position = new OpenTK.Mathematics.Vector2(400f, 300f);
        Object1.transform.Scale = new OpenTK.Mathematics.Vector2(50f, 50f);

        _mainScene.AddObject(Object1);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        
        Input.Keyboard = KeyboardState;
        Input.Mouse = MouseState;

        Time.deltaTime = (float)e.Time;

        SceneManager.InvokeUpdateComponents();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        SceneManager.InvokeRenderComponents();

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

        _renderSystem.Dispose();
    }
}