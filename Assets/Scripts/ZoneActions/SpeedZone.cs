using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour, IZoneAction
{
    public void ZoneAction()
    {
        CharacterManager.Instance.SpeedUp();
    }
}
