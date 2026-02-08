using System.Collections.Generic;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class SteamInventoryManager
{
    private Dictionary<int, Item> items = new Dictionary<int, Item>();
    private Dictionary<int, int> userInventory = new Dictionary<int, int>();

    public void AddItem(Item item)
    {
        items[item.Id] = item;
    }

    public void ConsumeItem(int itemId)
    {
        if(userInventory.ContainsKey(itemId))
        {
            userInventory[itemId]--;
            if (userInventory[itemId] <= 0)
            {
                userInventory.Remove(itemId);
            }
        }
    }

    public void DropItem(int itemId)
    {
        // Logic to drop an item
    }

    public void TradeItem(int fromUserId, int toUserId, int itemId)
    {
        // Trading logic
    }

    public event System.Action OnInventoryUpdated;

    private void TriggerInventoryUpdate()
    {
        OnInventoryUpdated?.Invoke();
    }
}