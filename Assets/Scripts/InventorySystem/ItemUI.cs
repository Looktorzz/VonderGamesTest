using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _itemCanvaGroup;
    public CanvasGroup ItemCanvaGroup => _itemCanvaGroup;

    [SerializeField] 
    private Image _itemImage;

    [SerializeField]
    private TextMeshProUGUI _stackAmountLabel;

    public ItemData ItemData { get; private set; }

    public int StackAmount { get; set; }

    public void SetUp(int id, int stackAmount)
    {
        ItemData = CoreGame.Instance.ItemDataConfig.GetItemDataById(id);
        StackAmount = stackAmount;

        _itemImage.sprite = ItemData.ItemSprite;
        _stackAmountLabel.text = StackAmount.ToString();
        ItemCanvaGroup.blocksRaycasts = true;
    }
}
