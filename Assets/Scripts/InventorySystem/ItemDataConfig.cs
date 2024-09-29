using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataConfig : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _itemDataList;
    public List<ItemData> ItemDataList => _itemDataList;

    public ItemData GetItemDataById(int id)
    {
        foreach (ItemData itemData in _itemDataList)
        {
            if (itemData.Id == id)
            {
                return itemData;
            }
        }

        Debug.LogAssertion($"Can't found item data from id : {id}.");
        return null;
    }
}
