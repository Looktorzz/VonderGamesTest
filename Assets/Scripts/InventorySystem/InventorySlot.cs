using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _slotImage;

    [SerializeField]
    private bool _isHotBar = false;

    public ItemUI ItemUI { get; private set; }
    public bool HasItem { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool isLeftMouseButton = eventData.button == PointerEventData.InputButton.Left;
        bool isOpenInventory = CoreGame.Instance.InventoryManager.IsOpenInventory;

        if (!isLeftMouseButton)
        {
            return;
        }

        if (isOpenInventory)
        {
            CoreGame.Instance.InventoryManager.SetCurrentHoldItem(this);
        }
        else if (_isHotBar)
        {
            CoreGame.Instance.InventoryManager.SelectSlot(this);
        }
    }

    public void SetSelectBackgroundUI(bool isSelected)
    {
        GameConfig gameConfig = CoreGame.Instance.GameConfig;

        _slotImage.color = isSelected ? gameConfig.SelectedSlotColor : gameConfig.NormalSlotColor;
    }

    public void SetItemInSlot(ItemUI item)
    {
        ItemUI = item;
        ItemUI.transform.SetParent(this.transform);
        ItemUI.transform.localScale = Vector3.one;
        ItemUI.transform.localPosition = Vector3.zero;
        ItemUI.ItemCanvaGroup.blocksRaycasts = false;
        HasItem = ItemUI != null;
    }

    public ItemUI ReturnItemInSlot()
    {
        ItemUI itemUI = ItemUI;
        ItemUI = null;
        HasItem = false;

        return itemUI;
    }
}
