using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private TimeHopController _timeHopController;

    public void Interact()
    {
        _timeHopController.OnTimeChanged();
    }
}
