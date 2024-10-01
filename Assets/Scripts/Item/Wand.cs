using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// TODO: Make this class a derived class of BaseEquipment.
public class Wand : MonoBehaviour
{
    [SerializeField]
    private ProjectileBullet _projectileBullet;

    public void OnInteract(bool isLeftDirection)
    {
        ProjectileBullet projectileBullet = Instantiate(_projectileBullet, transform.position, Quaternion.identity);
        projectileBullet.Setup(isLeftDirection);
    }
}
