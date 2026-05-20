namespace DungeonTextAdventure
{
    internal class PlayerClass
    {
        public string Name { get; set; }
        public int StartHp { get; set; }
        public int StartAttack { get; set; }
        public int StartSpeed { get; set; }

        public PlayerClass(string name, int startHp, int startAttack, int startSpeed)
        {
            Name = name;
            StartHp = startHp;
            StartAttack = startAttack;
            StartSpeed = startSpeed;
        }
    }
}
