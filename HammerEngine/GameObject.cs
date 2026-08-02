public abstract class GameObject
{
    public abstract void Destroy();
    ~GameObject()
    {
        Destroy();
    }
}
