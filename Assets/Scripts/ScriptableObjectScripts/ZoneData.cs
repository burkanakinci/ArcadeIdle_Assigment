using UnityEngine;

[CreateAssetMenu(fileName = "ZoneData", menuName = "Zone Data")]
public class ZoneData : ScriptableObject
{
    public int zoneId;
    public int zoneUnlockAmount = 10;
    public float dropCubeRate = 0.5f;

    public bool IsLocked
    {
        get
        {
            if (!PlayerPrefs.HasKey("IsLockedZone" + zoneId))
            {
                PlayerPrefs.SetInt("IsLockedZone" + zoneId, 1);
            }

            return PlayerPrefs.GetInt("IsLockedZone" + zoneId) == 1;

        }

        set => PlayerPrefs.SetInt("IsLockedZone" + zoneId, value ? 1 : 0);
    }
    
}
