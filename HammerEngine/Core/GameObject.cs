public abstract class GameObject
{
    public string Name { get; set; }
    public int Index { get; set; }

    List<Component> _components = new List<Component>();

    public GameObject(string name, int index)
    {
        Name = name;
        Index = index;
    }

    public virtual void Destroy()
    {
    }
}
