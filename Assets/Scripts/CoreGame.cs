using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreGame : Singleton<CoreGame>
{
    [SerializeField]
    private ItemDataConfig _itemDataConfig;
    public ItemDataConfig ItemDataConfig => _itemDataConfig;

    [SerializeField]
    private InventoryManager _inventoryManager;
    public InventoryManager InventoryManager => _inventoryManager;

    [SerializeField]
    private TimeHopController _timeHopController;

    [SerializeField]
    private TimeHopUI _timeHopUI;

    private void Start()
    {
        _timeHopController.Init();
        _timeHopUI.Setup();
    }
}
