using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HelperZone : MonoBehaviour, IZoneAction
{
    public Sprite unlockedIcon;
    private CubeAreaController tempCubeArea;
    [SerializeField] private ZoneController zoneController;
    [SerializeField] private Transform zoneArea;

    public void ZoneAction()
    {
        if (zoneController == null)
        {
            zoneController = GetComponent<ZoneController>();
        }

        zoneArea.DOPunchScale(transform.localScale * (1.08f),
            0.5f,
            0,
            0.0f).OnComplete(() => SpawnHelper());
    }
    private void SpawnHelper()
    {
        tempCubeArea = CubeAreaManager.Instance.UnlockCubeAreas(zoneController);
        ObjectPool.Instance.SpawnHelper((transform.position + Vector3.up * 2f), tempCubeArea);
    }
    public Sprite GetUnlockedIcon()
    {
        return unlockedIcon;
    }
}
