namespace DungeonTextAdventure
{
    internal class Game
    {
        private Random random;

        public Game()
        {
            random = new Random();
        }


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
            Console.WriteLine("Druecke eine Taste, um den Dungeon zu betreten...");
            Console.ReadKey();

            StartDungeon(player);

            Console.Clear();
            Console.WriteLine("Das Spiel wurde beendet.");
            Console.WriteLine();
            Console.WriteLine("Druecke eine Taste, um das Programm zu schließen...");
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

        private void StartDungeon(Player player)
        {
            int currentRoom;
            string input;
            bool dungeonIsRunning;
            bool validInput;

            currentRoom = 1;
            dungeonIsRunning = true;

            while (dungeonIsRunning == true)
            {
                validInput = false;

                while (validInput == false)
                {
                    Console.Clear();

                    Console.WriteLine("===== DUNGEON =====");
                    Console.WriteLine();
                    Console.WriteLine($"Aktueller Raum: {currentRoom}");
                    Console.WriteLine();
                    Console.WriteLine("Vor dir befinden sich zwei Türen.");
                    Console.WriteLine();
                    Console.WriteLine("1. Linke Tür öffnen");
                    Console.WriteLine("2. Rechte Tür öffnen");
                    Console.WriteLine("3. Stats anzeigen");
                    Console.WriteLine("4. Inventar anzeigen");
                    Console.WriteLine("5. Dungeon verlassen");
                    Console.WriteLine();

                    Console.Write("Deine Auswahl: ");
                    input = Console.ReadLine() ?? "";

                    if (input == "1")
                    {
                        Console.Clear();

                        Console.WriteLine("Du öffnest die linke Tür...");
                        Console.WriteLine();

                        currentRoom = currentRoom + 1;
                        validInput = true;

                        Console.WriteLine($"Du betrittst Raum {currentRoom}.");
                        Console.WriteLine();

                        GenerateRoomEvent(player, currentRoom);

                        if (player.IsDead())
                        {
                            dungeonIsRunning = false;
                        }
                    }
                    else if (input == "2")
                    {
                        Console.Clear();

                        Console.WriteLine("Du öffnest die rechte Tür...");
                        Console.WriteLine();

                        currentRoom = currentRoom + 1;
                        validInput = true;

                        Console.WriteLine($"Du betrittst Raum {currentRoom}.");
                        Console.WriteLine();

                        GenerateRoomEvent(player, currentRoom);

                        if (player.IsDead())
                        {
                            dungeonIsRunning = false;
                        }
                    }
                    else if (input == "3")
                    {
                        Console.Clear();

                        player.ShowStats();

                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste, um zur Tür-Auswahl zurückzukehren...");
                        Console.ReadKey();
                    }
                    else if (input == "4")
                    {
                        Console.Clear();

                        player.ShowInventory();

                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste, um zur Tür-Auswahl zurückzukehren...");
                        Console.ReadKey();
                    }
                    else if (input == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("Du verlässt den Dungeon.");
                        validInput = true;
                        dungeonIsRunning = false;
                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe. Bitte 1, 2, 3, 4 oder 5 eingeben.");
                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste, um es erneut zu versuchen...");
                        Console.ReadKey();
                    }
                }
            }
        }

        private void GenerateRoomEvent(Player player, int currentRoom)
        {
            int eventRoll;
            Enemy enemy;
            Item foundItem;

            eventRoll = random.Next(1, 101);

            Console.Clear();

            if (eventRoll <= 60)
            {
                Console.WriteLine("Du betrittst einen Gegnerraum!");
                Console.WriteLine();

                enemy = CreateRandomEnemy(currentRoom);

                Console.WriteLine($"Ein {enemy.Name} erscheint!");
                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste, um den Kampf zu starten...");
                Console.ReadKey();

                StartFight(player, enemy);
            }
            else if (eventRoll <= 85)
            {
                Console.WriteLine("Der Raum ist leer.");
                Console.WriteLine("Du kannst kurz durchatmen.");

                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
            }
            else
            {
                foundItem = CreateRandomItem();

                Console.WriteLine("Du findest etwas am Boden.");
                Console.WriteLine($"Gefunden: {foundItem.Name}");

                player.AddItem(foundItem);

                Console.WriteLine();
                Console.WriteLine($"{foundItem.Name} wurde deinem Inventar hinzugefügt.");

                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
            }
        }

        private Enemy CreateRandomEnemy(int currentRoom)
        {
            int enemyRoll;
            Enemy enemy;

            enemyRoll = random.Next(1, 101);

            if (currentRoom <= 2)
            {
                if (enemyRoll <= 70)
                {
                    enemy = new Goblin();
                }
                else
                {
                    enemy = new Skeleton();
                }
            }
            else if (currentRoom <= 4)
            {
                if (enemyRoll <= 80)
                {
                    enemy = new Skeleton();
                }
                else
                {
                    enemy = new Orc();
                }
            }
            else
            {
                if (enemyRoll <= 35)
                {
                    enemy = new Skeleton();
                }
                else
                {
                    enemy = new Orc();
                }
            }

            return enemy;
        }

        private void StartFight(Player player, Enemy enemy)
        {
            bool fightIsRunning;

            fightIsRunning = true;

            while (fightIsRunning == true)
            {
                Console.Clear();

                Console.WriteLine("Ein Gegner erscheint!");
                Console.WriteLine();

                player.ShowStats();
                Console.WriteLine();
                enemy.ShowStats();

                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste für die nächste Kampfrunde...");
                Console.ReadKey();

                Console.Clear();

                if (player.Speed >= enemy.Speed)
                {
                    Console.WriteLine($"{player.Name} ist schneller und greift zuerst an.");
                    enemy.TakeDamage(player.Attack);
                    Console.WriteLine($"{enemy.Name} verliert {player.Attack} HP.");

                    if (enemy.IsDead() == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{enemy.Name} greift zurueck.");
                        player.TakeDamage(enemy.Attack);
                        Console.WriteLine($"{player.Name} verliert {enemy.Attack} HP.");
                    }
                }
                else
                {
                    Console.WriteLine($"{enemy.Name} ist schneller und greift zuerst an.");
                    player.TakeDamage(enemy.Attack);
                    Console.WriteLine($"{player.Name} verliert {enemy.Attack} HP.");

                    if (player.IsDead() == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{player.Name} greift zurueck.");
                        enemy.TakeDamage(player.Attack);
                        Console.WriteLine($"{enemy.Name} verliert {player.Attack} HP.");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();

                if (player.IsDead())
                {
                    Console.Clear();
                    Console.WriteLine("Du wurdest besiegt.");
                    Console.WriteLine("Deine Reise endet hier.");

                    fightIsRunning = false;

                    Console.WriteLine();
                    Console.WriteLine("Drücke Enter, um fortzufahren...");
                    Console.ReadLine();

                }
                else if (enemy.IsDead())
                {
                    Console.Clear();

                    Console.WriteLine($"{enemy.Name} wurde besiegt.");
                    Console.WriteLine($"Du erhältst {enemy.ExpReward} EXP.");
                    Console.WriteLine();
                    Console.WriteLine("Der Weg durch den Dungeon geht weiter.");

                    fightIsRunning = false;

                    Console.WriteLine();
                    Console.WriteLine("Druecke Enter, um fortzufahren...");
                    Console.ReadLine();
                }
            }
        }

        private Item CreateRandomItem()
        {
            int itemRoll;
            Item item;

            itemRoll = random.Next(1, 101);

            if (itemRoll <= 60)
            {
                item = new Item("Kleiner Heiltrank", "Potion", "None", 30, 0, 0, 0, true);
            }
            else if (itemRoll <= 80)
            {
                item = new Item("Großer Heiltrank", "Potion", "None", 60, 0, 0, 0, true);
            }
            else if (itemRoll <= 90)
            {
                item = new Item("Rostiges Schwert", "Equipment", "Weapon", 0, 0, 2, 0, false);
            }
            else
            {
                item = new Item("Lederrüstung", "Equipment", "Armor", 0, 20, 0, 0, false);
            }

            return item;
        }
    }
}