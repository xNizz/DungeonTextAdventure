namespace DungeonTextAdventure
{
    internal class Player
    {
        public string Name { get; set; }
        public string ClassName { get; set; }

        public int Level { get; set; }
        public int Exp {  get; set; }
        public int ExpToNextLevel { get; set; }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }

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
    }
}
