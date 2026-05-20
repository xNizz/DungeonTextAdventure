namespace DungeonTextAdventure
{
    internal class Game
    {
        public void StartGame()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("      Dungeon Text Adventure      ");
            Console.WriteLine("==================================");
            Console.WriteLine();

            Console.WriteLine("Willkommen im Dungeon!");
            Console.WriteLine("Das Spiel wurde erfolgreich gestartet.");
            Console.WriteLine();

            Console.WriteLine("Druecke eine Taste, um das Spiel zu beenden...");
            Console.ReadKey();
        }
    }
}
