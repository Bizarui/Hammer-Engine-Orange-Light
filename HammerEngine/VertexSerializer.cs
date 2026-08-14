namespace HammerEngine
{
    internal static class VertexSerializer
    {
        public static float[] ToFloatArray(Vertex[] vertices)
        {
            int size = vertices.Length * 4;

            float[] result = new float[size];

            for (int i = 0; i < size; i += 4)
            {
                int v_index = i / 4;
                ref Vertex vertex = ref vertices[v_index];

                result[i] = vertex.Position.X;
                result[i + 1] = vertex.Position.Y;
                result[i + 2] = vertex.UiPosition.X;
                result[i + 3] = vertex.UiPosition.Y;
            }

            return result;
        }
    }
}
