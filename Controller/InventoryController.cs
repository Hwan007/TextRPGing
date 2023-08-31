using System.Collections.Generic;
using TextRPGing.Model;

namespace TextRPGing.Controller
{
    public class InventoryController
    {
        public Item[] GetAllItem()
        {
           return Character.Player.Inven.Items.ToArray();
        }

        public Item GetItem(int index)
        {
            List<Item> playerItems = Character.Player.Inven.Items;

            Item findItem = playerItems[index];

            return findItem;
        }

        public void RemoveItem(int index)
        {
            List<Item> playerItems = Character.Player.Inven.Items;
            if (playerItems[index] == null || playerItems.Count < index)
                return;

           Character.Player.Inven.Items.Remove(playerItems[index]);
        }

        public void AddItem(Item item)
        {
            if (item == null)
                return;

            Character.Player.Inven.Items.Add(item);
        }
    }
}
