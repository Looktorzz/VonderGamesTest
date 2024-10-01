using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    [SerializeField]
    private Image _inventorySlotGroupImage;

    [SerializeField]
    private Image _hotBarSlotsGroupImage;

    public bool IsOpenInventory { get; private set; }

    private bool _isInit;
    private ItemUI _currentHoldItem;
    private InventorySlot _currentSelectedSlot;

    private void Update()
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

    public void Init()
    {
        if (_isInit)
        {
            return;
        }

        GameConfig gameConfig = CoreGame.Instance.GameConfig;

        _inventorySlotGroupImage.color = gameConfig.CloseInventoryColor;
        _hotBarSlotsGroupImage.color = gameConfig.CloseInventoryColor;
        IsOpenInventory = false;

        _isInit = true;
    }

    public void OpenInventory()
    {
        if (_currentHoldItem != null)
        {
            return;
        }

        GameConfig gameConfig = CoreGame.Instance.GameConfig;
        Color openInventoryColor = gameConfig.OpenInventoryColor;
        Color closeInventoryColor = gameConfig.CloseInventoryColor;

        IsOpenInventory = !IsOpenInventory;
        _inventorySlotGroupImage.color = IsOpenInventory ? openInventoryColor : closeInventoryColor;
        _hotBarSlotsGroupImage.color = IsOpenInventory ? openInventoryColor : closeInventoryColor;
    }

    public void SelectSlot(InventorySlot inventorySlot)
    {
        if (_currentSelectedSlot != null)
        {
            _currentSelectedSlot.SetSelectBackgroundUI(false);
        }

        _currentSelectedSlot = inventorySlot;
        _currentSelectedSlot.SetSelectBackgroundUI(true);

        ItemData itemData = inventorySlot.HasItem ? inventorySlot.ItemUI.ItemData : null;
        CoreGame.Instance.Player.SetCurrentHoldItem(itemData);
    }

    public void TestSpawnItemUI()
    {
        ItemUI itemUI1 = Instantiate(_itemUIPrefab);
        itemUI1.SetUp(0, 3);
        AddItemInSlot(itemUI1);

        ItemUI itemUI2 = Instantiate(_itemUIPrefab);
        itemUI2.SetUp(1, 1);
        AddItemInSlot(itemUI2);
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

    public void RemoveItem()
    {
        if (_currentHoldItem == null)
        {
            return;
        }

        Destroy(_currentHoldItem.gameObject);
        _currentHoldItem = null;
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
