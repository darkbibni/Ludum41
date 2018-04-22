using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public Delegates.OnLootItem OnSteal;

    private List<Item> items = new List<Item>();
    
    public int ItemCount
    {
        get { return items.Count; }
    }

    public void AddItem(Item item)
    {
        items.Add(item);

        if(OnSteal != null)
        {
            OnSteal(item);
        }
    }

    public void LoseAllItems()
    {
        items.Clear();
    }
}
