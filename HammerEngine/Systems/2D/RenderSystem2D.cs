using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class RenderSystem2D
{
    public Shader MainShader { get; private set; }

    public RenderSystem2D(string vertexPath, string fragmentPath)
    {
        MainShader = new Shader(vertexPath, fragmentPath);
    }

    public void BeginFrame(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        MainShader.Use();
        MainShader.SetMatrix4("view", viewMatrix);
        MainShader.SetMatrix4("projection", projectionMatrix);
    }

    public void DrawMesh(MeshFilter2D mesh, Transform transform, SpriteRenderer2D renderer)
    {
        MainShader.SetMatrix4("model", transform.GetModelMatrix());

        Vector4 colorVec = new Vector4(renderer.Color.R, renderer.Color.G, renderer.Color.B, renderer.Color.A);
        MainShader.SetVector4("objectColor", colorVec);

        GL.BindVertexArray(mesh.VaoHandle);
        GL.DrawElements(PrimitiveType.Triangles, mesh.IndexCount, DrawElementsType.UnsignedInt, 0);
    }

    public void Dispose()
    {
        MainShader.Dispose();
    }
}
