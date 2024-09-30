using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveSlot : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CoreGame.Instance.InventoryManager.RemoveItem();
        }
    }
}
