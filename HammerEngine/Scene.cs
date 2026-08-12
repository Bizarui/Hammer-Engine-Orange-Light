namespace HammerEngine
{
    public class Scene
    {
        public int Index { get; }

        public IReadOnlyCollection<Component> AllComponents => GameObjects.SelectMany(x => x.Components).ToArray().AsReadOnly();

        public IReadOnlyCollection<GameObject> GameObjects => _gameObjects.AsReadOnly();
        private List<GameObject> _gameObjects = new List<GameObject>();

        internal Scene(int index)
        {
            Index = index;
        }

        public void AddObject(GameObject obj) => _gameObjects.Add(obj);
        public void RemoveObject(GameObject obj) => _gameObjects.Remove(obj);
    }
}