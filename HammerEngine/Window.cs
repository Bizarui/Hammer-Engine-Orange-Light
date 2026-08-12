using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using HammerEngine;

public class Game : GameWindow
{
    private ScreenSystem2D _screen;
    private RenderSystem2D _renderSystem;
    private Transform _figureTransform;
    private Camera2D _camera;
    private MeshFilter2D _figureMesh;
    private SpriteRenderer2D _figureRenderer;

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

        _camera = new Camera2D();
        _screen = new ScreenSystem2D(Size.X, Size.Y);
        _figureTransform = new Transform();
        _figureTransform.Position = new Vector2(400.0f, 300.0f);
        _figureMesh = AFG2D.CreateQuadMesh(
        new Vector3(100.0f, 100.0f, 0.0f),
        new Vector3(100.0f, -100.0f, 0.0f),
        new Vector3(-100.0f, -100.0f, 0.0f),
        new Vector3(-100.0f, 100.0f, 0.0f)
        );
        _figureRenderer = new SpriteRenderer2D(Color4.White);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        
        Input.Keyboard = KeyboardState;
        Input.Mouse = MouseState;

        Time.deltaTime = (float)e.Time;

        _camera.Update();

        SceneManager.InvokeUpdateComponents();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        _renderSystem.BeginFrame(_camera.GetViewMatrix(), _screen.GetProjectionMatrix());
        _renderSystem.DrawMesh(_figureMesh, _figureTransform, _figureRenderer);

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

        _figureMesh.Destroy();
        _renderSystem.Dispose();
    }
}