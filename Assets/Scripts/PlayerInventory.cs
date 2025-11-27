using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public bool HasItem(string id)
    {
        return items.Contains(id);
    }

    public void AddItem(string id)
    {
        if (!items.Contains(id))
            items.Add(id);
    }
}
