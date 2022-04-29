using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    public CubeData cubeData;
    private Vector3 jumpTarget, tempSpawnPos;
    private CubeAreaController parentCubeArea;

    public IEnumerator SpawnCube()
    {
        yield return new WaitForSeconds(cubeData.cubeSpawnRate);
        //ObjectSpawn on ObjectPool
        ObjectPool.Instance.SpawnCollactableCube(tempSpawnPos,parentCubeArea);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&
                CharacterManager.Instance.GetCollectedCubeCount()<30)
        {
            tempSpawnPos = transform.position;

            //SpawnCube
            StartCoroutine(SpawnCube());

            //AddList
            CharacterManager.Instance.AddCollectedCube(this);

            //Jump cube
            transform.DOLocalJump(CharacterManager.Instance.CalculateCubeTarget(), cubeData.cubeJumpPower, 1, cubeData.cubeJumpDuration);
            CharacterManager.Instance.ResetStackValues();
            transform.DOLocalRotate(Vector3.zero, cubeData.cubeJumpDuration);
            CubeAreaManager.Instance.DecreaseCubeOnCubeArea(this);
        }
    }
    public void DropCube()
    {
        transform.DOLocalJump(Vector3.zero, cubeData.cubeJumpPower, 1, cubeData.cubeJumpDuration)
        .OnComplete(() => DropComplete());
    }
    private void DropComplete()
    {
        transform.DOPunchScale(Vector3.one*(-1),0.7f,1).OnComplete(()=> ObjectPool.Instance.ClearCollectableCube(this));
        
    }
    public void SetParentCubeArea(CubeAreaController _cubeArea)
    {
        parentCubeArea = _cubeArea;
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
