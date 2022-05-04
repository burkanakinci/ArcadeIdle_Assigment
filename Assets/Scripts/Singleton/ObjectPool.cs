using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
            }
            return instance;
        }
    }

    [SerializeField] private List<CubeController> collactableCubePool = new List<CubeController>();
    [SerializeField] private CubeController collactableCubePrefab;
    private CubeController tempCube;

    [SerializeField] private List<HelperController> helperPool = new List<HelperController>();
    [SerializeField] private HelperController helperPrefab;
    private HelperController tempHelper ;

    private void Awake()
    {
        instance = this;
    }
    public void SpawnCollactableCube(Vector3 _spawnPos, CubeAreaController _parentCubeArea)
    {

        if (collactableCubePool.Count > 0)
        {
            tempCube = collactableCubePool[0];
            tempCube.gameObject.SetActive(true);
            tempCube.gameObject.layer = 6;
            tempCube.transform.SetParent(_parentCubeArea.GetCubeAreaCubesParent());
            tempCube.transform.position = _spawnPos;
            tempCube.transform.localEulerAngles = Vector3.zero;
            collactableCubePool.Remove(tempCube);

            _parentCubeArea.AddCubeList(tempCube);

            tempCube.SetParentCubeArea(_parentCubeArea);

            return;
        }

            collactableCubePrefab.gameObject.layer = 6;
            tempCube= Instantiate(collactableCubePrefab, _spawnPos, Quaternion.identity, _parentCubeArea.GetCubeAreaCubesParent());
        tempCube.SetParentCubeArea(_parentCubeArea);

        _parentCubeArea.AddCubeList(tempCube);
       
    }
    public void SpawnHelper(Vector3 _spawnPos,CubeAreaController _cubeArea)
    {
        if (helperPool.Count > 0)
        {
            tempHelper = helperPool[0];
            tempHelper.gameObject.SetActive(true);

            tempHelper.transform.position = _spawnPos;

            collactableCubePool.Remove(tempCube);

            tempHelper.SetTargetCubes(_cubeArea.GetCubes());
            return;
        }

        tempHelper = Instantiate(helperPrefab, _spawnPos, Quaternion.identity);
        tempHelper.SetTargetCubes(_cubeArea.GetCubes());
    }

    public float GetCubeZScale()
    {
        return collactableCubePrefab.transform.localScale.z;
    }
    public float GetCubeYScale()
    {
        return collactableCubePrefab.transform.localScale.y;
    }
    public float GetCubeXScale()
    {
        return collactableCubePrefab.transform.localScale.x;
    }
    public void ClearCollectableCube(CubeController _cube)
    {
        _cube.gameObject.SetActive(false);
        collactableCubePool.Add(_cube);
    }
}