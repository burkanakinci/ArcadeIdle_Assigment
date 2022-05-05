using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityZone : MonoBehaviour, IZoneAction
{
    public void ZoneAction()
    {
        CharacterManager.Instance.CapacityUp();
    }
}
