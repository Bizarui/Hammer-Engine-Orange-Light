using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

public class AFG2D  // automatic figure generation 2D
{
    private int VertexBufferObject;
    private int VertexArrayObject;
    private int ElementBufferObject;

    private Vector3[] _points = new Vector3[4];

    uint[] indices = {
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
    };

    public AFG2D (Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    {
        _points[0] = point1;
        _points[1] = point2;
        _points[2] = point3;
        _points[3] = point4;

        InitBuffers();
    }

    private void InitBuffers()
    {
        // 1. Создаем и активируем VAO
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        // 2. Создаем VBO
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

        GL.BufferData(BufferTarget.ArrayBuffer, _points.Length * Vector3.SizeInBytes, _points, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // 3. Создаем и заполняем EBO
        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        // Отвязываем VAO
        GL.BindVertexArray(0);
    }

    public void Draw()
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void Destroy()
    {
        GL.DeleteBuffer(VertexBufferObject);
        GL.DeleteBuffer(ElementBufferObject);
        GL.DeleteVertexArray(VertexArrayObject);
    }
}