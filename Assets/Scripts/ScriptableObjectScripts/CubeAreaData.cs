using UnityEngine;

[CreateAssetMenu(fileName = "CubeAreaData", menuName = "Cube Area Data")]
public class CubeAreaData : ScriptableObject
{
    public int cubeAreaId;
    public float cubeWidthOffset=1f,cubeLengthOffset=1f;
    public int widthCubeCount;
    public int lengthCubeCount;

    public bool IsLocked
    {
        get
        {
            if (!PlayerPrefs.HasKey("IsLockedCubeArea" + cubeAreaId))
            {
                PlayerPrefs.SetInt("IsLockedCubeArea" + cubeAreaId, 1);
            }

            return PlayerPrefs.GetInt("IsLockedCubeArea" + cubeAreaId) == 1;

        }

        set => PlayerPrefs.SetInt("IsLockedCubeArea" + cubeAreaId, value ? 1 : 0);
    }

}
