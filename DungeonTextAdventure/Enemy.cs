namespace DungeonTextAdventure
{
    internal class Enemy
    {
        public string Name { get; set; }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

        public int Attack { get; set; }
        public int Speed { get; set; }

        public int ExpReward { get; set; }

        public Enemy(string name, int maxHp, int attack, int speed, int expReward)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            Attack = attack;
            Speed = speed;
            ExpReward = expReward;
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

        public void ShowStats()
        {
            Console.WriteLine("===== GEGNERSTATS =====");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"HP: {CurrentHp} / {MaxHp}");
            Console.WriteLine($"ANG: {Attack}");
            Console.WriteLine($"SPD: {Speed}");
            Console.WriteLine($"EXP: {ExpReward}");
            Console.WriteLine("=======================");
        }
    }
}
