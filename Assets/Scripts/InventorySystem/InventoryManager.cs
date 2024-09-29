using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<InventorySlot> _hotbarSlots;

    [SerializeField]
    private List<InventorySlot> _inventorySlots;

    [SerializeField]
    private ItemUI _itemUIPrefab;

    [SerializeField] 
    private Canvas _canvas;

    private ItemUI _currentHoldItem;

    void Update()
    {
        if (_currentHoldItem == null)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            Input.mousePosition,
            _canvas.worldCamera,
            out Vector2 localPoint
        );

        _currentHoldItem.transform.localPosition = localPoint;
    }

    public void TestSpawnItemUI()
    {
        ItemUI itemUI = Instantiate(_itemUIPrefab);
        itemUI.SetUp(0, 3);
        AddItemInSlot(itemUI);
    }

    public void AddItemInSlot(ItemUI item)
    {
        foreach (InventorySlot slot in _hotbarSlots)
        {
            if (slot.HasItem)
            {
                continue;
            }

            slot.SetItemInSlot(item);
            return;
        }

        // If the hotbar slot is full, the item will be added to the inventory slot.
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.HasItem)
            {
                continue;
            }

            slot.SetItemInSlot(item);
            return;
        }

        Debug.Log($"Cannot add items: Inventory is full.");
    }

    public void SetCurrentHoldItem(InventorySlot slot)
    {
        if (_currentHoldItem == null)
        {
            if (slot.HasItem)
            {
                _currentHoldItem = slot.ReturnItemInSlot();
                _currentHoldItem.transform.SetParent(_canvas.transform);
                _currentHoldItem.ItemCanvaGroup.blocksRaycasts = false;
            }

            return;
        }

        // Slot contains an item that can be fully merged.
        if (slot.HasItem && TryToMergeItemUI(slot.ItemUI, _currentHoldItem))
        {
            // Destroy current hold item.
            Destroy(_currentHoldItem.gameObject);
            _currentHoldItem = null;
        }

        // Slot is empty.
        if (!slot.HasItem)
        {
            // Set current hold item into slot.
            slot.SetItemInSlot(_currentHoldItem);
            _currentHoldItem = null;
        }
    }

    public bool TryToMergeItemUI(ItemUI itemInSlot, ItemUI newItem)
    {
        if (itemInSlot == null || newItem == null)
        {
            return false;
        }

        int itemStackAmount = itemInSlot.StackAmount;
        int newItemStackAmount = newItem.StackAmount;
        int maxStackAmount = itemInSlot.ItemData.MaxStack;

        bool isSameId = itemInSlot.ItemData.Id == newItem.ItemData.Id;
        bool isStackable = itemStackAmount + newItemStackAmount <= maxStackAmount;

        if (!isSameId)
        {
            return false;
        }

        if (isStackable)
        {
            itemStackAmount += newItem.StackAmount;
            itemInSlot.SetStackAmount(itemStackAmount);

            return true;
        }

        newItemStackAmount -= maxStackAmount - itemStackAmount;
        itemStackAmount = maxStackAmount;
        newItem.SetStackAmount(newItemStackAmount);
        itemInSlot.SetStackAmount(itemStackAmount);

        return false;
    }
}
