using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.Timers;

public class Game : GameWindow
{
    Shader shader;

    private Stopwatch timer = Stopwatch.StartNew();

    int VertexBufferObject;
    int VertexArrayObject;
    int ElementBufferObject;

    private readonly float[] vertices =
   {
      // positions        // colors
      0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
     -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
      0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
    };

    uint[] indices = {  // Внимание: индексация точек начинается с 0!
    0, 1, 3,   // Первый треугольник: верхний-правый -> нижний-правый -> верхний-левый
    1, 2, 3    // Второй треугольник: нижний-правый -> нижний-левый -> верхний-левый
    };

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

        // VAO
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        // VBO
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // Настраиваем указатели атрибутов для активного VBO
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // EBO
        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        // Очищаем экран фоновым цветом
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Включаем шейдер
        shader.Use();

        // Подключаем наш настроенный квадрат
        GL.BindVertexArray(VertexArrayObject);

        // Отрисовка КВАДРАТА по индексам из EBO
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        // Выводим кадр на экран
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        shader.Dispose();
    }
}

