namespace DungeonTextAdventure
{
    internal class Game
    {
        public void StartGame()
        {
            Player player;

            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("      Dungeon Text Adventure      ");
            Console.WriteLine("==================================");
            Console.WriteLine();

            player = CreatePlayer();

            Console.Clear();

            player.ShowStats();

            Console.WriteLine();
            Console.WriteLine("Druecke eine Taste, um fortzufahren...");
            Console.ReadKey();
        }

        private Player CreatePlayer()
        {
            string playerName;
            string input;
            bool validInput;
            PlayerClass selectedClass;
            Player player;

            validInput = false;

            selectedClass = new PlayerClass("Tank", 140, 8, 6);

            Console.Clear();

            Console.WriteLine("Willkommen im Dungeon!");
            Console.WriteLine();

            Console.Write("Gib deinen Namen ein: ");
            playerName = Console.ReadLine() ?? "";

            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Write("Name darf nicht leer sein. Bitte erneut eingeben: ");
                playerName = Console.ReadLine() ?? "";
            }

            Console.Clear();

            while (validInput == false)
            {
                Console.Clear();

                Console.WriteLine("Wähle deine Klasse:");
                Console.WriteLine();

                Console.WriteLine("1.   Tank");
                Console.WriteLine("     Mehr HP, weniger ANG und SPD");
                Console.WriteLine();

                Console.WriteLine("2.   DPS");
                Console.WriteLine("     Mehr ANG und SPD, weniger HP");
                Console.WriteLine();

                Console.Write("Deine Auswahl: ");
                input = Console.ReadLine() ?? "";

                if (input == "1")
                {
                    selectedClass = new PlayerClass("Tank", 140, 8, 6);
                    validInput = true;
                }
                else if (input == "2")
                {
                    selectedClass = new PlayerClass("DPS", 90, 14, 10);
                    validInput = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Ungültige Eingabe. Bitte 1 oder 2 eingeben.");
                    Console.WriteLine("Drücke eine Taste, um es erneut zu versuchen...");
                    Console.ReadKey();
                }
            }

            player = new Player(playerName, selectedClass);

            return player;
        }
    }
}