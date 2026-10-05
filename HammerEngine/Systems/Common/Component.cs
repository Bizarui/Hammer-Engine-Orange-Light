public abstract class Component
{
    public GameObject gameObject { get; private set; }

    internal void SetGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public Component? GetComponent<T>() where T : notnull, Component => gameObject.GetComponent<T>();
    public bool TryGetComponent<T>(out T? component) where T : notnull, Component => gameObject.TryGetComponent<T>(out component);
    public T AddComponent<T>() where T : notnull, Component, new() => gameObject.AddComponent<T>();
    public Component AddComponent(Type type) => gameObject.AddComponent(type);
    public void RemoveComponent<T>() where T : notnull, Component => gameObject.RemoveComponent<T>();

    public virtual void Awake() { }
    public virtual void Update() { }
    public virtual void Render() { }
    public virtual void Destroy() { }
}
