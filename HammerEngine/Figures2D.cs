using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

public static class AFG2D  // automatic figure generation 2D
{
    public static MeshFilter2D CreateTriangleMesh(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vector3[] points = new Vector3[] { point1, point2, point3 };
        uint[] indices = new uint[] { 0, 1, 2 };

        return InitBuffers(points, indices);
    }

    public static MeshFilter2D CreateQuadMesh(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    {
        Vector3[] points = new Vector3[] { point1, point2, point3, point4 };

        uint[] indices = new uint[] {
            0, 1, 3,
            1, 2, 3
        };

        return InitBuffers(points, indices);
    }

    private static MeshFilter2D InitBuffers(Vector3[] points, uint[] indices)
    {
        int vao = GL.GenVertexArray();
        GL.BindVertexArray(vao);

        int vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, points.Length * Vector3.SizeInBytes, points, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        int ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(0);

        return new MeshFilter2D(vao, vbo, ebo, indices.Length);
    }
}