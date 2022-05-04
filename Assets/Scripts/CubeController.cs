using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    public CubeData cubeData;
    private Vector3 tempSpawnPos;
    private CubeAreaController parentCubeArea;
    private bool jumpOnZone;
    private ZoneController zoneController;

    public IEnumerator SpawnCube()
    {
        yield return new WaitForSeconds(cubeData.cubeSpawnRate);
        //ObjectSpawn on ObjectPool
        ObjectPool.Instance.SpawnCollactableCube(tempSpawnPos, parentCubeArea);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
                CharacterManager.Instance.GetCollectedCubeCount() < 30)
        {
            tempSpawnPos = transform.position;
            //SpawnCube
            if (!StorageManager.Instance.IsOnStorage(this))
            {

                StartCoroutine(SpawnCube());
            }
            else
            {
                StorageManager.Instance.DecreaseStorageCube(this);
                StorageManager.Instance.AddStorageCollectedPos(tempSpawnPos);
            }

            //AddList
            CharacterManager.Instance.AddCollectedCube(this);

            //Jump cube
            CharacterManager.Instance.ResetStackValues();
            transform.DOLocalJump(CharacterManager.Instance.CalculateCubeTarget(), cubeData.cubeJumpPower, 1, cubeData.cubeJumpDuration);
            transform.DOLocalRotate(Vector3.zero, cubeData.cubeJumpDuration);
            CubeAreaManager.Instance.DecreaseCubeOnCubeArea(this);
        }
    }
    public void DropCube(ZoneController _zone, Vector3 _target)
    {
        zoneController = _zone;

        transform.DOLocalRotate(Vector3.zero, cubeData.cubeJumpDuration);
        transform.DOLocalJump(_target, cubeData.cubeJumpPower, 1, cubeData.cubeJumpDuration)
        .OnComplete(() => StartCoroutine(DropComplete()));
    }
    private IEnumerator DropComplete()
    {

        if (zoneController == null)
        {
            transform.DOPunchScale(transform.localScale * (cubeData.cubePunchScaleMultiplier), cubeData.cubePunchScaleDuration, 0, 0.0f);
        }
        else
        {
            zoneController.SetUnlockText();
        }
        yield return new WaitForSeconds(cubeData.cubePunchScaleDuration);

        if (zoneController != null)
        {
            ObjectPool.Instance.ClearCollectableCube(this);
        }

        this.gameObject.layer = 6;
    }
    public void SetParentCubeArea(CubeAreaController _cubeArea)
    {
        parentCubeArea = _cubeArea;
    }
    private void OnDisable()
    {
        transform.DOKill();
    }

    public void SetSpawnPos(Vector3 _spawnPos)
    {
        tempSpawnPos = _spawnPos;
    }
}
