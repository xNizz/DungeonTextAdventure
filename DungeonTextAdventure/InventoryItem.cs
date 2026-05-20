namespace DungeonTextAdventure
{
    internal class InventoryItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public InventoryItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
