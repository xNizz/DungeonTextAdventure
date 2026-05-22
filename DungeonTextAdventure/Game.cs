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

            ShowTitle();

            player = CreatePlayer();

            Console.Clear();

            player.ShowStats();

            PauseWithKey("Drücke eine Taste, um den Dungeon zu betreten...");

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
            int selectedOption;
            bool dungeonIsRunning;
            bool bossWasDefeated;
            string[] dungeonOptions;

            currentRoom = 1;
            dungeonIsRunning = true;

            dungeonOptions = new string[]
            {
                "Linke Tür öffnen",
                "Rechte Tür öffnen",
                "Stats anzeigen",
                "Inventar anzeigen",
                "Dungeon verlassen"
            };

            while (dungeonIsRunning == true)
            {
                selectedOption = ShowSelectionMenu("DUNGEON", dungeonOptions);

                if (selectedOption == 0)
                {
                    Console.Clear();

                    Console.WriteLine("Du öffnest die linke Tür...");
                    Console.WriteLine();

                    currentRoom = currentRoom + 1;

                    Console.WriteLine($"Du betrittst Raum {currentRoom}.");

                    PauseWithKey("Drücke eine Taste, um fortzufahren...");

                    bossWasDefeated = GenerateRoomEvent(player, currentRoom);

                    if (player.IsDead() || bossWasDefeated)
                    {
                        dungeonIsRunning = false;
                    }
                }
                else if (selectedOption == 1)
                {
                    Console.Clear();

                    Console.WriteLine("Du öffnest die rechte Tür...");
                    Console.WriteLine();

                    currentRoom = currentRoom + 1;

                    Console.WriteLine($"Du betrittst Raum {currentRoom}.");

                    PauseWithKey("Drücke eine Taste, um fortzufahren...");

                    bossWasDefeated = GenerateRoomEvent(player, currentRoom);

                    if (player.IsDead() || bossWasDefeated)
                    {
                        dungeonIsRunning = false;
                    }
                }
                else if (selectedOption == 2)
                {
                    Console.Clear();

                    player.ShowStats();

                    PauseWithKey("Drücke eine Taste, um zur Tür-Auswahl zurückzukehren...");
                }
                else if (selectedOption == 3)
                {
                    OpenInventory(player);
                }
                else if (selectedOption == 4)
                {
                    Console.Clear();

                    Console.WriteLine("Du verlässt den Dungeon.");

                    dungeonIsRunning = false;

                    PauseWithKey("Drücke eine Taste, um fortzufahren...");
                }
            }
        }
        

        private bool GenerateRoomEvent(Player player, int currentRoom)
        {
            int eventRoll;
            int bossRoll;
            Enemy enemy;
            Item foundItem;
            bool bossWasDefeated;

            bossWasDefeated = false;

            Console.Clear();

            bossRoll = random.Next(1, 101);

            if (currentRoom >= 8 || (currentRoom >= 5 && bossRoll <= 20))
            {
                Console.WriteLine("Du findest eine große, dunkle Tür.");
                Console.WriteLine("Hinter ihr spürst du eine starke Präsenz...");
                Console.WriteLine();
                Console.WriteLine("Du hast den Bossraum gefunden!");
                Console.WriteLine();

                enemy = new Boss();

                Console.WriteLine($"Der Boss erscheint: {enemy.Name}");
                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste, um den Bosskampf zu starten...");
                Console.ReadKey();

                StartFight(player, enemy);

                if (enemy.IsDead() && player.IsDead() == false)
                {
                    Console.Clear();

                    Console.WriteLine("Du hast den Boss besiegt!");
                    Console.WriteLine("Der Dungeon wurde erfolgreich abgeschlossen.");
                    Console.WriteLine();
                    Console.WriteLine("Du hasst gewonnen!");

                    bossWasDefeated = true;

                    Console.WriteLine();
                    Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                    Console.ReadKey();
                }

                return bossWasDefeated;
            }

            eventRoll = random.Next(1, 101);

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

            return bossWasDefeated;
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

                    player.GainExp(enemy.ExpReward);

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

            if (itemRoll <= 45)
            {
                item = new Item("Kleiner Heiltrank", "Potion", "None", 30, 0, 0, 0, true);
            }
            else if (itemRoll <= 65)
            {
                item = new Item("Großer Heiltrank", "Potion", "None", 60, 0, 0, 0, true);
            }
            else if (itemRoll <= 80)
            {
                item = new Item("Rostiges Schwert", "Equipment", "Weapon", 0, 0, 2, 0, false);
            }
            else if (itemRoll <= 90)
            {
                item = new Item("Lederrüstung", "Equipment", "Armor", 0, 20, 0, 0, false);
            }
            else
            {
                item = new Item("Leichte Stiefel", "Equipment", "Boots", 0, 0, 0, 2, false);
            }

            return item;
        }

        private void OpenInventory(Player player)
        {
            int selectedOption;
            int inventoryIndex;
            bool inventoryIsOpen;
            string[] inventoryOptions;

            inventoryIsOpen = true;

            inventoryOptions = new string[]
            {
        "Item benutzen",
        "Ausrüstung anlegen",
        "Zurück"
            };

            while (inventoryIsOpen == true)
            {
                selectedOption = ShowSelectionMenu("INVENTAR", inventoryOptions);

                if (selectedOption == 0)
                {
                    Console.Clear();

                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("Dein Inventar ist leer.");
                        PauseWithKey("Drücke eine Taste, um zum Inventar zurückzukehren...");
                    }
                    else
                    {
                        inventoryIndex = ShowInventoryItemSelection(player, "ITEM BENUTZEN");

                        if (inventoryIndex != -1)
                        {
                            Console.Clear();

                            player.UsePotion(inventoryIndex);

                            PauseWithKey("Drücke eine Taste, um zum Inventar zurückzukehren...");
                        }
                    }
                }
                else if (selectedOption == 1)
                {
                    Console.Clear();

                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("Dein Inventar ist leer.");
                        PauseWithKey("Drücke eine Taste, um zum Inventar zurückzukehren...");
                    }
                    else
                    {
                        inventoryIndex = ShowInventoryItemSelection(player, "AUSRÜSTUNG ANLEGEN");

                        if (inventoryIndex != -1)
                        {
                            Console.Clear();

                            player.EquipItem(inventoryIndex);

                            PauseWithKey("Drücke eine Taste, um zum Inventar zurückzukehren...");
                        }
                    }
                }
                else if (selectedOption == 2)
                {
                    inventoryIsOpen = false;
                }
            }
        }

        private int ShowInventoryItemSelection(Player player, string title)
        {
            string[] itemOptions;
            int selectedIndex;
            int i;

            itemOptions = new string[player.Inventory.Count + 1];

            for (i = 0; i < player.Inventory.Count; i++)
            {
                itemOptions[i] = CreateInventoryOptionText(player.Inventory[i]);
            }

            itemOptions[itemOptions.Length - 1] = "Zurück";

            selectedIndex = ShowSelectionMenu(title, itemOptions);

            if (selectedIndex == itemOptions.Length - 1)
            {
                return -1;
            }

            return selectedIndex;
        }

        private string CreateInventoryOptionText(InventoryItem inventoryItem)
        {
            Item item;
            string optionText;

            item = inventoryItem.Item;

            if (item.Type == "Potion")
            {
                optionText = $"{item.Name} x{inventoryItem.Quantity} | heilt {item.HealAmount} HP";
            }
            else if (item.Type == "Equipment")
            {
                if (item.Slot == "Weapon")
                {
                    optionText = $"{item.Name} x{inventoryItem.Quantity} | Waffe | ANG +{item.AttackBonus}";
                }
                else if (item.Slot == "Armor")
                {
                    optionText = $"{item.Name} x{inventoryItem.Quantity} | Rüstung | HP +{item.HpBonus}";
                }
                else if (item.Slot == "Boots")
                {
                    optionText = $"{item.Name} x{inventoryItem.Quantity} | Schuhe | SPD +{item.SpeedBonus}";
                }
                else
                {
                    optionText = $"{item.Name} x{inventoryItem.Quantity} | Ausrüstung";
                }
            }
            else
            {
                optionText = $"{item.Name} x{inventoryItem.Quantity}";
            }

            return optionText;
        }


        //Hilfsmethoden

        private void PauseWithKey(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
        }

        private void PauseWithEnter(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadLine();
        }

        private void ShowTitle()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("      Dungeon Text Adventure      ");
            Console.WriteLine("==================================");
            Console.WriteLine();
        }

        private void WriteColoredLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private int ShowSelectionMenu(string title, string[] options)
        {
            int selectedIndex;
            int i;
            bool selectionConfirmed;
            ConsoleKeyInfo keyInfo;

            selectedIndex = 0;
            selectionConfirmed = false;

            while (selectionConfirmed == false)
            {
                Console.Clear();

                Console.WriteLine($"===== {title} =====");
                Console.WriteLine();

                for (i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {options[i]}");
                    }
                    else
                    {
                        Console.WriteLine($" {options[i]}");
                    }
                }

                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = selectedIndex - 1;

                    if (selectedIndex < 0)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = selectedIndex + 1;

                    if (selectedIndex >= options.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    selectionConfirmed = true;
                }
            }

            return selectedIndex;
        }
    }
}