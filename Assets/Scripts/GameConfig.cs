using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField]
    private Color _openInventoryColor;
    public Color OpenInventoryColor => _openInventoryColor;

    [SerializeField]
    private Color _closeInventoryColor;
    public Color CloseInventoryColor => _closeInventoryColor;

    [SerializeField]
    private Color _normalSlotColor;
    public Color NormalSlotColor => _normalSlotColor;

    [SerializeField]
    private Color _selectedSlotColor;
    public Color SelectedSlotColor => _selectedSlotColor;
}
