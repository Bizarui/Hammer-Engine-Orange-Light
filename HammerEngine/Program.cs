namespace HammerEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "SuperMegaUltraGame"))
            {
                game.Run();
            }
        }
    }
}