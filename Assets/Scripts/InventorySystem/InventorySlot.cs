using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemUI ItemUI { get; private set; }
    public bool HasItem { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CoreGame.Instance.InventoryManager.SetCurrentHoldItem(this);
        }
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
