namespace DungeonTextAdventure
{
    internal class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slot { get; set; }

        public int HealAmount { get; set; }
        public int HpBonus {  get; set; }
        public int AttackBonus { get; set; }
        public int SpeedBonus { get; set; }
        
        public bool IsStackable { get; set; }

        public Item(string name, string type, string slot, int healAmount, int hpBonus, int attackBonus, int speedBonus, bool isStackable)
        {
            Name = name;
            Type = type;
            Slot = slot;
            HealAmount = healAmount;
            HpBonus = hpBonus;
            AttackBonus = attackBonus;
            SpeedBonus = speedBonus;
            IsStackable = isStackable;
        }
    }
}
