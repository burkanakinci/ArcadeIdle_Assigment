using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class ZoneController : MonoBehaviour
{
    [SerializeField] private ZoneData zoneData;
    [SerializeField] private Transform droppedCubePoint;
    [SerializeField] private BoxCollider lockedAreaCollider;
    private int tempCount, showCount;
    public IZoneAction zoneAction;
    private float timer;

    [SerializeField] private TextMeshPro zoneUnlockText;

    private void Start()
    {
        zoneAction = GetComponent<IZoneAction>();

        zoneUnlockText.text = "?";
        ResetZoneValue();
        if (GetZoneId() == 0)
        {
            CanDropped();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (GetIsLockZone() || CharacterManager.Instance.GetCollectedCubeCount() < 1 ||
                tempCount >= zoneData.zoneUnlockAmount)
                return;


            if (timer < zoneData.dropCubeRate)
            {
                timer += Time.deltaTime;
                return;
            }

            CharacterManager.Instance.DecreaseCube(ref droppedCubePoint, this);

            tempCount++;

            timer = 0.0f;

            if (tempCount >= zoneData.zoneUnlockAmount)
            {

                lockedAreaCollider.enabled = false;

                SetIsCompletedZone(true);

                zoneAction.ZoneAction();
            }
        }
    }

    private void ResetZoneValue()
    {
        tempCount = 0;
        showCount = -1;
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
        showCount++;
        zoneUnlockText.text = (zoneData.zoneUnlockAmount - showCount).ToString();
    }

    public bool GetIsLockZone()
    {
        return zoneData.IsLocked;
    }
    public void SetIsLockZone(bool _isLock)
    {
        zoneData.IsLocked = _isLock;
    }
    public bool GetIsCompletedZone()
    {
        return zoneData.IsCompleted;
    }
    public void SetIsCompletedZone(bool _isComplete)
    {
        zoneData.IsCompleted = _isComplete;
    }
    public int GetZoneId()
    {
        return zoneData.zoneId;
    }

}
