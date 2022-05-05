using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedZone : MonoBehaviour, IZoneAction
{
    public void ZoneAction()
    {
        ZoneAreaManager.Instance.UnlockZones();
    }
}
