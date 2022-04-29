using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAreaController : MonoBehaviour
{
    [SerializeField] private CubeAreaData cubeAreaData;
    [SerializeField] private Transform cubeAreaCubesParent;
    [SerializeField] private List<CubeController> cubesOnCubeArea = new List<CubeController>();


    private Vector3 tempSpawnPos;

    private void Start()
    {
        if (GetCubeAreaId() == 2)
        {
            SetCubeAreaIsLock(false);
        }
        if (!GetCubeAreaIsLock())
        {
            FillCubeArea();
        }
    }

    public void FillCubeArea()
    {
        for (int i = cubeAreaData.lengthCubeCount; i > 0; i--)
        {
            for (int j = cubeAreaData.widthCubeCount; j > 0; j--)
            {
                tempSpawnPos =
                    cubeAreaCubesParent.position +
                    new Vector3(
                        -(j * ObjectPool.Instance.GetCubeXScale() + j * cubeAreaData.cubeWidthOffset),
                        0.0f,
                        (i * ObjectPool.Instance.GetCubeZScale() + i * cubeAreaData.cubeLengthOffset));

                ObjectPool.Instance.SpawnCollactableCube(tempSpawnPos,this);
                
            }
        }
    }

    public int GetCubeAreaId()
    {
        return cubeAreaData.cubeAreaId;
    }
    public bool GetCubeAreaIsLock()
    {
        return cubeAreaData.IsLocked;
    }
    public void SetCubeAreaIsLock(bool _isLock)
    {
        cubeAreaData.IsLocked = _isLock;
    }
    public List<CubeController> GetCubes()
    {
        return cubesOnCubeArea;
    }
    public Transform GetCubeAreaCubesParent()
    {
        return cubeAreaCubesParent;
    }
    public void AddCubeList(CubeController _cubeController)
    {
        cubesOnCubeArea.Add(_cubeController);
    }
}
