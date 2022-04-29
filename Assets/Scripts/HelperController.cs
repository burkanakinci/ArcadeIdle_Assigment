using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelperController : MonoBehaviour
{
    
    public MovementStateMachine movementStateMachine;
    [SerializeField] private Animator helperAnimator;
    [SerializeField] private HelperData helperData;

    private int stackYCount, stackZCount;

    [SerializeField] private List<CubeController> targetCubes = new List<CubeController>();
    [SerializeField]private List<CubeController> collectedCubes = new List<CubeController>();
    [SerializeField] private Transform collectedCubeParent;
    private CubeController tempCube;



    private void Onenable()
    {
        stackYCount = 0;
        stackZCount = -1;
    }

    public void SetTargetCubes(List<CubeController> _cubes)
    {
        targetCubes = _cubes;
    }
    public Vector3 GetLastCubePos()
    {
        if(targetCubes.Count>0)
        {
            return targetCubes[targetCubes.Count - 1].transform.position;
        }
        return transform.position;
    }
    public void PlayWalkAnimation()
    {
        helperAnimator.SetBool("IsWalking",true);
    }
    public void PlayIdleAnimation()
    {
        helperAnimator.SetBool("IsWalking", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollactableCube"))
        {
                if(collectedCubes.Count<10)
            {
                for (int i = targetCubes.Count - 1; i >= 0; i--)
                {
                    if (targetCubes[i].transform == other.transform)
                    {
                        tempCube = targetCubes[i];
                        AddCollectedCube(tempCube);
                        targetCubes.Remove(tempCube);
                        StartCoroutine(tempCube.SpawnCube());
                        break;
                    }
                }


                //Jump cube
                other.transform.DOLocalJump(CalculateCubeTarget(), tempCube.cubeData.cubeJumpPower, 1, tempCube.cubeData.cubeJumpDuration);
                ResetStackValues();
                other.transform.DOLocalRotate(Vector3.zero, tempCube.cubeData.cubeJumpDuration);
                CubeAreaManager.Instance.DecreaseCubeOnCubeArea(tempCube);

                if (collectedCubes.Count >= 10)
                {
                    //movementStateMachine.ChangeState(movementStateMachine.movingStorageState);
                }
            }
        }
    }
    public void AddCollectedCube(CubeController _cube)
    {
        collectedCubes.Add(_cube);
        _cube.transform.parent = collectedCubeParent;
        _cube.gameObject.layer = 7;

        stackZCount++;
    }

    public Vector3 CalculateCubeTarget()
    {
        return new Vector3(((stackZCount - 1) * ObjectPool.Instance.GetCubeXScale()) + ((stackZCount - 1) * helperData.stackOffset),
            (stackYCount * ObjectPool.Instance.GetCubeYScale()) + (stackYCount * helperData.stackOffset),
            0.0f);
    }

    public void ResetStackValues()
    {
        if (collectedCubes.Count % 3 == 0)
        {
            stackYCount++;
            stackZCount = -1;
        }
    }

    public void DecreaseCube(ref Transform _droppedPoint)
    {

        tempCube = collectedCubes[collectedCubes.Count - 1];
        collectedCubes.Remove(tempCube);
        tempCube.transform.SetParent(_droppedPoint);
        tempCube.DropCube();

        stackYCount = collectedCubes.Count / 3;
        stackZCount = (collectedCubes.Count % 3) - 1;

    }
    public int GetCollectedCubeCount()
    {
        return collectedCubes.Count;
    }

    public int GetTargetCubeCount()
    {
        return targetCubes.Count;
    }
}
