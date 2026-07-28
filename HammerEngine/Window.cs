using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Diagnostics;

public class Game : GameWindow
{
    private Shader shader;

    private int VertexBufferObject;
    private int VertexArrayObject;

    // Массив вершин для ОДНОГО красивого цветного треугольника
    private readonly float[] vertices =
    {
      // Координаты (X, Y, Z) | Цвета (R, G, B)
       0.5f, -0.5f, 0.0f,       1.0f, 0.0f, 0.0f,   // Нижний правый угол (Красный)
      -0.5f, -0.5f, 0.0f,       0.0f, 1.0f, 0.0f,   // Нижний левый угол  (Зелёный)
       0.0f,  0.5f, 0.0f,       0.0f, 0.0f, 1.0f    // Верхний угол       (Синий)
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

        // Твой красивый тёмно-зелёный цвет фона
        GL.ClearColor(0.3f, 0.4f, 0.3f, 1.0f);

        // 1. Создаем и активируем VAO
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        // 2. Создаем VBO и загружаем в него массив vertices
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        // 3. Атрибут №0: Координаты (X, Y, Z). Шаг 6 float, смещение 0.
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // 4. Атрибут №1: Цвета (R, G, B). Шаг 6 float, смещение 3 float (пропускаем координаты).
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // Отвязываем VAO для безопасности, его настройка завершена
        GL.BindVertexArray(0);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        // Очищаем экран фоновым цветом
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Включаем шейдерную программу
        shader.Use();

        // Подключаем наш настроенный VAO перед отрисовкой
        GL.BindVertexArray(VertexArrayObject);

        // Рисуем треугольник: начинаем с 0-й вершины, всего берём 3 вершины
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        // Выводим готовый кадр на экран
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        // Освобождаем ресурсы видеокарты, чтобы не было утечек памяти
        GL.DeleteBuffer(VertexBufferObject);
        GL.DeleteVertexArray(VertexArrayObject);

        shader.Dispose();
    }
}