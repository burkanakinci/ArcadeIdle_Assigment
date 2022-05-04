using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }
    private List<CubeController> collectedCubes = new List<CubeController>();
    private CubeController tempCube;
    [SerializeField] private Transform collectedCubeParent;
    private int stackYMultiplier, stackXMultiplier;
    [SerializeField] private float stackOffset = 0.5f;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIController.Instance.SetStackCountText(collectedCubes.Count);

        ResetStackValues();
    }

    public void AddCollectedCube(CubeController _cube)
    {
        collectedCubes.Add(_cube);
        _cube.transform.parent = collectedCubeParent;
        _cube.gameObject.layer = 7;

        UIController.Instance.SetStackCountText(collectedCubes.Count);
    }

    public Vector3 CalculateCubeTarget()
    {
        return new Vector3(((stackXMultiplier) * ObjectPool.Instance.GetCubeXScale()) + ((stackXMultiplier) * stackOffset),
            (stackYMultiplier * ObjectPool.Instance.GetCubeYScale()) + (stackYMultiplier * stackOffset),
            0.0f) + (Vector3.right * (ObjectPool.Instance.GetCubeXScale() + stackOffset));
    }

    public void ResetStackValues()
    {
        stackYMultiplier = (collectedCubes.Count - 1) / 3;
        stackXMultiplier = (collectedCubes.Count % 3) - 2;
    }

    public void DecreaseCube(ref Transform _droppedPoint)
    {

        tempCube = collectedCubes[collectedCubes.Count - 1];
        collectedCubes.Remove(tempCube);
        tempCube.transform.SetParent(_droppedPoint);
        tempCube.DropCube(true, Vector3.zero);

        ResetStackValues();

        UIController.Instance.SetStackCountText(collectedCubes.Count);

    }

    public int GetCollectedCubeCount()
    {
        return collectedCubes.Count;
    }
}
