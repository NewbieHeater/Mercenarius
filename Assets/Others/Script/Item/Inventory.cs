using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public int stack;
        public InventoryItem(string itemName)
        {
            this.itemName = itemName;
            stack = 1;
        }
    }

    public class Bag
    {
        public List<InventoryItem> items = new List<InventoryItem>();
    }

    Bag bag;

    public void AddItem(SOItem soItem)
    {
        var item = bag.items.Where(x => x.itemName == soItem.name).FirstOrDefault();
        if (item != null && item.stack < soItem.maxStack)
        {
            item.stack++;
        }
        else
        {
            bag.items.Add(new InventoryItem(soItem.name));
            Save();
        }
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(bag);
        PlayerPrefs.SetString("SaveData", json);
    }

    public void Load()
    {
        var json = PlayerPrefs.GetString("SaveData", "");
        if (json == "")
        {
            bag = new Bag();
        }
        else
        {
            bag = JsonUtility.FromJson<Bag>(json);
        }
    }
}