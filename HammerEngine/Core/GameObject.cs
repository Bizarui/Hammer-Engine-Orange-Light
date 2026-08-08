using System.Reflection;

public class GameObject
{
    public string Name { get; set; }

    private List<Component> _components = new List<Component>();

    public GameObject(string name)
    {
        Name = name;
    }

    public Component? GetComponent<T>() where T : notnull, Component
    {
        return _components.FirstOrDefault(c => c is T);
    }

    public bool TryGetComponent<T>(out T? component) where T : notnull, Component
    {
        Component? foundComponent = GetComponent<T>();

        if (foundComponent == null)
        {
            component = null;
            return false;
        }

        component = foundComponent as T;
        return true;
    }

    public Component AddComponent<T>() where T : notnull, Component, new()
    {
        if (TryGetComponent<T>(out var component1))
            return component1;


        Component component = new T();
        component.SetGameObject(this);
        _components.Add(component);
        component.Awake();

        return component;
    }

    public Component AddComponent(Type type)
    {
        if (!type.IsSubclassOf(typeof(Component)))
        {
            throw new ArgumentException($"Класс \"{type.Name}\" - не компонент.");
        }

        if (type.IsAbstract || type.IsInterface)
            throw new ArgumentException($"Компонент \"{type.Name}\" не может быть: абстрактным или интерфейсом.");

        var component1 = _components.FirstOrDefault(c => c.GetType() == type);
        if (component1 != null)
            return component1;

        var constructors = type.GetConstructors(BindingFlags.Public);

        var constructor = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);

        if (constructor == null)
            throw new ArgumentException($"У компонента \"{type.Name}\" нет открытого конструктора без параметров.");

        Component? component = constructor.Invoke(null) as Component;

        if (component == null)
            throw new ArgumentException($"У компонента \"{type.Name}\" не удалось вызвать конструктор.");

        component.SetGameObject(this);
        _components.Add(component);
        component.Awake();

        return component;
    }

    public void RemoveComponent<T>() where T : notnull, Component
    {
        _components.RemoveAll(c => c is T);
    }

    public void Destroy()
    {
        foreach (var component in _components)
        {
            component.Destroy();
        }

        OnDestroy();
    }

    protected virtual void OnDestroy()
    {

    }
}
