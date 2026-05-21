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

        public Item? EquippedWeapon { get; set; }
        public Item? EquippedArmor { get; set; }
        public Item? EquippedBoots { get; set; }

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

            EquippedWeapon = null;
            EquippedArmor = null;
            EquippedBoots = null;
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

            if (CurrentHp > MaxHp)
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
            Console.WriteLine("===== AUSRÜSTUNG =====");
            Console.WriteLine();

            if (EquippedWeapon == null)
            {
                Console.WriteLine("Waffe: Keine");
            }
            else
            {
                Console.WriteLine($"Waffe: {EquippedWeapon.Name}");
            }

            if (EquippedArmor == null)
            {
                Console.WriteLine("Rüstung: Keine");
            }
            else
            {
                Console.WriteLine($"Rüstung: {EquippedArmor.Name}");
            }

            if (EquippedBoots == null)
            {
                Console.WriteLine("Schuhe: Keine");
            }
            else
            {
                Console.WriteLine($"Schuhe: {EquippedBoots.Name}");
            }

                Console.WriteLine();
            Console.WriteLine("====================");
        }

        public bool UsePotion(int inventoryIndex)
        {
            InventoryItem inventoryItem;
            Item item;
            int oldHp;
            int healedAmount;

            if (inventoryIndex < 0 || inventoryIndex >= Inventory.Count)
            {
                Console.WriteLine("Dieses Item existiert nicht.");

                return false;
            }

            inventoryItem = Inventory[inventoryIndex];
            item = inventoryItem.Item;

            if (item.Type != "Potion")
            {
                Console.WriteLine("Dieses Item kann aktuell nicht benutzt werden.");

                return false;
            }

            if (CurrentHp == MaxHp)
            {
                Console.WriteLine("Deine Hp sind bereits voll.");

                return false;
            }

            oldHp = CurrentHp;

            Heal(item.HealAmount);

            healedAmount = CurrentHp - oldHp;

            inventoryItem.Quantity = inventoryItem.Quantity - 1;

            if (inventoryItem.Quantity <= 0)
            {
                Inventory.RemoveAt(inventoryIndex);
            }

            Console.WriteLine($"{item.Name} benutzt.");
            Console.WriteLine($"Du heilst {healedAmount} HP.");

            return true;
        }

        public bool EquipItem(int inventoryIndex)
        {
            InventoryItem inventoryItem;
            Item item;

            if (inventoryIndex < 0 || inventoryIndex >= Inventory.Count)
            {
                Console.WriteLine("Dieses Item existiert nicht.");

                return false;
            }

            inventoryItem = Inventory[inventoryIndex];
            item = inventoryItem.Item;

            if (item.Type != "Equipment")
            {
                Console.WriteLine("Dieses Item kann nicht ausgerüstet werden.");

                return false;
            }

            if (item.Slot == "Weapon")
            {
                if (EquippedWeapon != null)
                {
                    Attack = Attack - EquippedWeapon.AttackBonus;
                }

                EquippedWeapon = item;
                Attack = Attack + item.AttackBonus;

                Console.WriteLine($"{item.Name} wurde als Waffe ausgerüstet.");
                Console.WriteLine($"ANG +{item.AttackBonus}");
            }
            else if (item.Slot == "Armor")
            {
                if (EquippedArmor != null)
                {
                    MaxHp = MaxHp - EquippedArmor.HpBonus;

                    if (CurrentHp > MaxHp)
                    {
                        CurrentHp = MaxHp;
                    }
                }

                EquippedArmor = item;
                MaxHp = MaxHp + item.HpBonus;
                CurrentHp = CurrentHp + item.HpBonus;

                if (CurrentHp > MaxHp)
                {
                    CurrentHp = MaxHp;
                }

                Console.WriteLine($"{item.Name} wurde als Rüstung ausgerüstet.");
                Console.WriteLine($"HP +{item.HpBonus}");
            }
            else if (item.Slot == "Boots")
            {
                if (EquippedBoots != null)
                {
                    Speed = Speed - EquippedBoots.SpeedBonus;
                }

                EquippedBoots = item;
                Speed = Speed + item.SpeedBonus;

                Console.WriteLine($"{item.Name} wurden als Schuhe ausgerüstet.");
                Console.WriteLine($"SPD +{item.SpeedBonus}");
            }
            else
            {
                Console.WriteLine("Dieses Ausrüstungsteil hat keinen gültigen Slot.");

                return false;
            }

            return true;
        }
    }
}
