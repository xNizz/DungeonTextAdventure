namespace DungeonTextAdventure
{
    internal class Player
    {
        public string Name { get; set; }
        public string ClassName { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpToNextLevel { get; set; }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }

        public List<InventoryItem> Inventory { get; set; }

        public Player(string name, PlayerClass playerClass)
        {
            Name = name;

            ClassName = playerClass.Name;

            Level = 1;
            Exp = 0;
            ExpToNextLevel = 100;

            MaxHp = playerClass.StartHp;
            CurrentHp = playerClass.StartHp;
            Attack = playerClass.StartAttack;
            Speed = playerClass.StartSpeed;

            Inventory = new List<InventoryItem>();
        }

        public void ShowStats()
        {
            Console.WriteLine("===== SPIELERSTATS =====");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Klasse: {ClassName}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"HP: {CurrentHp} / {MaxHp}");
            Console.WriteLine($"ANG: {Attack}");
            Console.WriteLine($"SPD: {Speed}");
            Console.WriteLine($"EXP: {Exp} / {ExpToNextLevel}");
            Console.WriteLine("========================");
        }

        public void TakeDamage(int damage)
        {
            CurrentHp = CurrentHp - damage;

            if (CurrentHp < 0)
            {
                CurrentHp = 0;
            }

        }

        public bool IsDead()
        {
            return CurrentHp <= 0;
        }

        public void Heal(int healAmount)
        {
            CurrentHp = CurrentHp + healAmount;

            if (CurrentHp > 0)
            {
                CurrentHp = MaxHp;
            }
        }


        public void AddItem(Item item)
        {
            bool itemWasStacked;
            int i;

            itemWasStacked = false;

            if (item.IsStackable == true)
            {
                for (i = 0; i < Inventory.Count; i++)
                {
                    if (Inventory[i].Item.Name == item.Name)
                    {
                        Inventory[i].Quantity = Inventory[i].Quantity + 1;
                        itemWasStacked = true;
                        break;
                    }
                }
            }

            if (itemWasStacked == false)
            {
                Inventory.Add(new InventoryItem(item, 1));
            }
        }

        public void ShowInventory()
        {
            int i;

            Console.WriteLine("===== INVENTAR =====");
            Console.WriteLine();

            if (Inventory.Count == 0) 
            {
                Console.WriteLine("Dein Inventar ist Leer.");
            }
            else
            {
                for (i = 0; i < Inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Inventory[i].Item.Name} x{Inventory[i].Quantity}");
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("====================");
        }
    }
}
