using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id => _id;

    [SerializeField]
    private string _itemName;
    public string ItemName => _itemName;

    [SerializeField]
    private Sprite _itemSprite;
    public Sprite ItemSprite => _itemSprite;

    [SerializeField]
    private int _maxStack;
    public int MaxStack => _maxStack;
}
