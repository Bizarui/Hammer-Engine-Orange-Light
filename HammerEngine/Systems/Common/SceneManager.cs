namespace HammerEngine.Systems.Common
{
    public static class SceneManager
    {
        public static Scene CurrentScene { get; private set; }
        private static Dictionary<int, Scene> _scenes = new Dictionary<int, Scene>();

        public static Scene CreateScene()
        {
            if (_scenes.Count == 0)
            {
                Scene scene1 = new Scene(0);
                _scenes.Add(0, scene1);

                return scene1;
            }

            int index = _scenes.Last().Key + 1;

            Scene scene2 = new Scene(index);
            _scenes.Add(index, scene2);

            return scene2;
        }

        public static Scene LoadScene(int index)
        {
            if (_scenes.Count == 0)
            {
                throw new IndexOutOfRangeException("Вы не создали ни одной сцены.");
            }

            if (_scenes.ContainsKey(index))
            {
                Scene scene = _scenes[index];

                CurrentScene = scene;

                return CurrentScene;
            }else
            {
                throw new ArgumentException("Сцена с таким айди не существует.");
            }
        }

        internal static void InvokeUpdateComponents()
        {
            if (CurrentScene == null)
                return;

            foreach (Component component in CurrentScene.AllComponents)
            {
                component.Update();
            }
        }

        internal static void InvokeRenderComponents()
        {
            if (CurrentScene == null)
                return;

            foreach (Component component in CurrentScene.AllComponents)
            {
                component.Render();
            }
        }
    }
}
