using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityZone : MonoBehaviour, IZoneAction
{
    public Sprite unlockedIcon;
    public void ZoneAction()
    {
        CharacterManager.Instance.CapacityUp();
    }

    public Sprite GetUnlockedIcon()
    {
        return unlockedIcon;
    }
}
