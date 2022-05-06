using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour, IZoneAction
{
    public Sprite unlockedIcon;
    public void ZoneAction()
    {
        CharacterManager.Instance.SpeedUp();
    }

        public Sprite GetUnlockedIcon()
    {
        return unlockedIcon;
    }
}
