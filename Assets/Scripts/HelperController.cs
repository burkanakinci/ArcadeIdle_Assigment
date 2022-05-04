using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{

    public MovementStateMachine movementStateMachine;
    [SerializeField] private Animator helperAnimator;
    [SerializeField] private HelperData helperData;

    private int stackYMultiplier, stackXMultiplier;

    [SerializeField] private List<CubeController> targetCubes = new List<CubeController>();
    [SerializeField] private List<CubeController> collectedCubes = new List<CubeController>();
    [SerializeField] private Transform collectedCubeParent;
    private CubeController tempCube;
    [SerializeField] private NavMeshAgent helperNavMesh;
    private float timer;

    private void OnEnable()
    {
        timer = 0.0f;
        ResetStackValues();
    }
    public void PlayWalkAnimation()
    {
        helperAnimator.SetBool("IsWalking", true);
    }
    public void PlayIdleAnimation()
    {
        helperAnimator.SetBool("IsWalking", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollactableCube"))
        {
            if (collectedCubes.Count < 10)
            {
                for (int i = targetCubes.Count - 1; i >= 0; i--)
                {
                    if (targetCubes[i].transform == other.transform)
                    {
                        tempCube = targetCubes[i];
                        AddCollectedCube(tempCube);
                        targetCubes.Remove(tempCube);

                        movementStateMachine.SetTarget();

                        tempCube.SetSpawnPos(other.transform.position);
                        StartCoroutine(tempCube.SpawnCube());

                        //Jump cube
                        ResetStackValues();
                        other.transform.DOLocalJump(CalculateCubeTarget(), tempCube.cubeData.cubeJumpPower, 1, tempCube.cubeData.cubeJumpDuration);
                        other.transform.DOLocalRotate(Vector3.zero, tempCube.cubeData.cubeJumpDuration);
                        CubeAreaManager.Instance.DecreaseCubeOnCubeArea(tempCube);

                        if (collectedCubes.Count >= 10)
                        {
                            helperNavMesh.SetDestination(StorageManager.Instance.GetDroppedAreaPos());
                            //movementStateMachine.ChangeState(movementStateMachine.movingStorageState);
                        }

                        break;
                    }
                }
            }
        }
        else if (other.CompareTag("Storage"))
        {
            timer = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Storage"))
        {

            if (StorageManager.Instance.GetStorageCubeCount() >= 40 ||
                collectedCubes.Count < 1)
                return;


            if (timer < helperData.dropCubeRate)
            {
                timer += Time.deltaTime;
                return;
            }

            DecreaseCube();

            timer = 0.0f;
        }
    }
    public void AddCollectedCube(CubeController _cube)
    {
        collectedCubes.Add(_cube);
        _cube.transform.parent = collectedCubeParent;
        _cube.gameObject.layer = 7;
    }

    public Vector3 CalculateCubeTarget()
    {
        return new Vector3(((stackXMultiplier) * ObjectPool.Instance.GetCubeXScale()) + ((stackXMultiplier) * helperData.stackOffset),
            (stackYMultiplier * ObjectPool.Instance.GetCubeYScale()) + (stackYMultiplier * helperData.stackOffset),
            0.0f) + (Vector3.right * (ObjectPool.Instance.GetCubeXScale() + helperData.stackOffset));
    }

    public void ResetStackValues()
    {
        stackYMultiplier = (collectedCubes.Count - 1) / 3;
        stackXMultiplier = (collectedCubes.Count % 3) - 2;
    }

    public void DecreaseCube()
    {

        tempCube = collectedCubes[collectedCubes.Count - 1];
        collectedCubes.Remove(tempCube);
        tempCube.transform.SetParent(StorageManager.Instance.storageCubeParent);
        StorageManager.Instance.AddStorageCube(tempCube);

        tempCube.DropCube(false, StorageManager.Instance.GetCubeTargetPos());

        ResetStackValues();


        if (collectedCubes.Count == 0)
        {
            SetNavMeshTarget();
        }

    }
    public void SetTargetCubes(List<CubeController> _cubes)
    {
        targetCubes = _cubes;

        if (movementStateMachine.GetCurrentState() != movementStateMachine.movingState &&
            GetTargetCubeCount() > 0)
        {
            movementStateMachine.ChangeState(movementStateMachine.movingState);
        }
    }
    public int GetCollectedCubeCount()
    {
        return collectedCubes.Count;
    }

    public int GetTargetCubeCount()
    {
        return targetCubes.Count;
    }

    public void SetNavMeshTarget()
    {
        helperNavMesh.SetDestination(targetCubes[0].transform.position);
    }

}
