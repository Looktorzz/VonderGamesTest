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

        // Assuming you have a reference to your Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
        Input.mousePosition,
        _canvas.worldCamera, // Pass your canvas's world camera, or null if it uses Screen Space - Overlay
            out Vector2 localPoint
        );

        _currentHoldItem.transform.localPosition = localPoint;


        /*float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;
        float currentPositionZ = _currentHoldItem.transform.position.z;
        _currentHoldItem.transform.position = new Vector3(mousePositionX, mousePositionY, currentPositionZ);*/
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
                _currentHoldItem.ItemCanvaGroup.blocksRaycasts = false;
            }

            return;
        }

        // Slot contains an item that can be fully merged.
        if (slot.HasItem && TryToMergeItemUI(slot.ItemUI, _currentHoldItem))
        {
            // Destroy current hold item.
            Destroy(_currentHoldItem);
            _currentHoldItem = null;
        }

        // Slot is empty.
        if (!slot.HasItem)
        {
            // Set current hold item into slot.
            slot.SetItemInSlot(_currentHoldItem);
            _currentHoldItem = null;
        }


        // Test
        /*_currentHoldItem = item;
        if (item != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != item.myItem.itemTag)
            {
                return;
            }
            item.activeSlot.SetItem(item);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        { EquipEquipment(item.activeSlot.myTag, null); }

        item = item;
        item.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);*/
    }

    public bool TryToMergeItemUI(ItemUI itemInSlot, ItemUI newItem)
    {
        if (itemInSlot == null || newItem == null)
        {
            return false;
        }

        bool isSameId = itemInSlot.ItemData.Id == newItem.ItemData.Id;
        bool isStackable = itemInSlot.StackAmount + newItem.StackAmount <= itemInSlot.ItemData.MaxStack;

        if (!isSameId)
        {
            return false;
        }

        if (isStackable)
        {
            itemInSlot.StackAmount += newItem.StackAmount;
            return true;
        }

        newItem.StackAmount -= itemInSlot.ItemData.MaxStack - itemInSlot.StackAmount;
        itemInSlot.StackAmount = itemInSlot.ItemData.MaxStack;
        return false;
    }
}
