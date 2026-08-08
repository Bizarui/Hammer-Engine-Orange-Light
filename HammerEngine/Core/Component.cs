public abstract class Component
{
    public GameObject GameObject { get; internal set; }

    public virtual void Awake() { }
    public virtual void Update(float deltaTime) { }
    public virtual void Render() { }
    public virtual void OnDestroy() { }
}
