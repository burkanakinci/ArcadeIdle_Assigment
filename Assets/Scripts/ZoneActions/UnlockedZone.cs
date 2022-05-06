using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedZone : MonoBehaviour, IZoneAction
{
    public Sprite unlockedIcon;
    public void ZoneAction()
    {
        ZoneAreaManager.Instance.UnlockZones();
    }

    public Sprite GetUnlockedIcon()
    {
        return unlockedIcon;
    }
}
