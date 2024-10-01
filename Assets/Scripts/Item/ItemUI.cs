using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO: Make this class is abstract class.
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

    public int StackAmount { get; private set; }

    public void SetUp(int id, int stackAmount)
    {
        ItemData = CoreGame.Instance.ItemDataConfig.GetItemDataById(id);
        StackAmount = stackAmount;

        _itemImage.sprite = ItemData.ItemSprite;
        _stackAmountLabel.text = StackAmount.ToString();
        ItemCanvaGroup.blocksRaycasts = true;
    }

    public void SetStackAmount(int stackAmount)
    {
        StackAmount = stackAmount;
        _stackAmountLabel.text = StackAmount.ToString();
    }
}
