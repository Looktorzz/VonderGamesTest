using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    [SerializeField]
    private TimeHopController _timeHopController;

    [SerializeField]
    private TimeHopUI _timeHopUI;

    private void Awake()
    {
        _timeHopController.Init();
        _timeHopUI.Setup();
    }
}
