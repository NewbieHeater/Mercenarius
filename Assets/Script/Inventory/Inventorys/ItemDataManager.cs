using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : InventoryBase
{

    void Start()
    {
        
    }
    
    public string GetName(int id)
    {
        return mSlots[id].Item.name;
    }

    public string GetDescription(int id)
    {
        return mSlots[id].Item.Description;
    }
}
