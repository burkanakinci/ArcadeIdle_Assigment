using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelperZone : MonoBehaviour, IZoneAction
{
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
}
