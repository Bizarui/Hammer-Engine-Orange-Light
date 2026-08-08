public abstract class Component
{
    public GameObject GameObject { get; private set; }

    internal void SetGameObject(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public Component? GetComponent<T>() where T : notnull, Component => GameObject.GetComponent<T>();
    public bool TryGetComponent<T>(out T? component) where T : notnull, Component => GameObject.TryGetComponent<T>(out component);
    public Component AddComponent<T>() where T : notnull, Component, new() => GameObject.AddComponent<T>();
    public Component AddComponent(Type type) => GameObject.AddComponent(type);
    public void RemoveComponent<T>() where T : notnull, Component => GameObject.RemoveComponent<T>();

    public virtual void Awake() { }
    public virtual void Update() { }
    public virtual void Destroy() { }
}
