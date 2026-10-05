using OpenTK.Mathematics;

namespace HammerEngine.Systems.Common
{
    public struct Vertex
    {
        public Vector2 Position = Vector2.One;
        public Vector2 UiPosition = Vector2.One;

        public Vertex(Vector2 position, Vector2 uiPosition)
        {
            Position = position;
            UiPosition = uiPosition;
        }

        public Vertex(Vector2 position) : this(position, position) { }
    }
}
