using OpenTK.Graphics.OpenGL4;

namespace HammerEngine.Components2D
{
    public class RenderPrimitive : Component
    {
        public Shader Shader { get; set; } = Shader.Defualt;

        private Vertex[] _vertices;

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        internal virtual void Init(Vertex[] vertices)
        {
            _vertices = vertices;

            float[] buffer = VertexSerializer.ToFloatArray(_vertices);

            _vertexArrayObject = GL.GenVertexArray();
            _vertexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, buffer.Length * sizeof(float), buffer, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        public override void Render()
        {
            Shader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}
