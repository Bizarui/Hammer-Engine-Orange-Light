public class MeshFilter2D
{
    public int VaoHandle { get; private set; }
    public int VboHandle { get; private set; }
    public int EboHandle { get; private set; }

    public int IndexCount { get; private set; }

    public MeshFilter2D(int vao, int vbo, int ebo, int indexCount)
    {
        VaoHandle = vao;
        VboHandle = vbo;
        EboHandle = ebo;
        IndexCount = indexCount;
    }

    public void CleanUp()
    {
        OpenTK.Graphics.OpenGL4.GL.DeleteVertexArray(VaoHandle);
        OpenTK.Graphics.OpenGL4.GL.DeleteBuffer(VboHandle);
        OpenTK.Graphics.OpenGL4.GL.DeleteBuffer(EboHandle);
    }
}
