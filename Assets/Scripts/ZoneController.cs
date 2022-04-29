using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class ZoneController : MonoBehaviour
{
    [SerializeField] private ZoneData zoneData;
    [SerializeField] private Transform droppedCubePoint;
    [SerializeField] private BoxCollider lockedAreaCollider;
    [SerializeField]private int tempCount;
    private CubeAreaController tempCubeArea;


    private float timer;

    [SerializeField]private TextMeshPro zoneUnlockText;

    private void Start()
    {
        zoneUnlockText.text = "?";
        if(GetZoneId()==0)
        {
            
            CanDropped();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (GetIsLockZone() || CharacterManager.Instance.GetCollectedCubeCount() < 1||
                tempCount>= zoneData.zoneUnlockAmount)
                return;


            if (timer < zoneData.dropCubeRate)  
            {
                timer += Time.deltaTime;
                return;
            }

            CharacterManager.Instance.DecreaseCube(ref droppedCubePoint);

            tempCount++;
            SetUnlockText();

            timer = 0.0f;

            if (tempCount>=zoneData.zoneUnlockAmount)
            {

                DropCompleted();

            }
        }
    }

    private void DropCompleted()
    {

        if (GetZoneId() == 0)
            ZoneAreaManager.Instance.UnlockZones();
        else if(GetZoneId() == 2|| GetZoneId() == 4|| GetZoneId() ==6)
        {

            //Helper üret ve helper'a hedefini söyle
            ResetZoneValue();
            tempCubeArea = CubeAreaManager.Instance.UnlockCubeAreas(this);
            ObjectPool.Instance.SpawnHelper((transform.position+Vector3.up*2f),tempCubeArea);
        }

        lockedAreaCollider.enabled = false;
    }
    private void ResetZoneValue()
    {
        tempCount = 0;
    }

    public void CanDropped()
    {
        //Change sprite text vs.

        timer = 0.0f;
        tempCount = 0;

        SetIsLockZone(false);

        SetUnlockText();
    }
    public void SetUnlockText()
    {
        zoneUnlockText.text = (zoneData.zoneUnlockAmount-tempCount).ToString();
    }

    public bool GetIsLockZone()
    {
        return zoneData.IsLocked;
    }
    public void SetIsLockZone(bool _isLock)
    {
        zoneData.IsLocked = _isLock;
    }
    public int GetZoneId()
    {
        return zoneData.zoneId;
    }

}
