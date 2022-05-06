using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HelperZone : MonoBehaviour, IZoneAction
{
    public Sprite unlockedIcon;
    private CubeAreaController tempCubeArea;
    private ZoneController zoneController;
    [SerializeField] private Transform zoneArea;
    private void Start()
    {
        zoneController = GetComponent<ZoneController>();
    }
    public void ZoneAction()
    {
        tempCubeArea = CubeAreaManager.Instance.UnlockCubeAreas(zoneController);
        ObjectPool.Instance.SpawnHelper((transform.position + Vector3.up * 2f), tempCubeArea);

        zoneArea.DOPunchScale(transform.localScale * (1.12f),
            0.8f,
            0,
            0.0f);
    }
    public Sprite GetUnlockedIcon()
    {
        return unlockedIcon;
    }
}
