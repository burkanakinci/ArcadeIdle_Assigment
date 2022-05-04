using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageManager : MonoBehaviour
{
    private static StorageManager instance;
    public static StorageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("StorageManager").AddComponent<StorageManager>();
            }
            return instance;
        }
    }
    public Transform storageCubeParent;
    [SerializeField] private Transform storageDroppedArea;
    private List<CubeController> storageCubes = new List<CubeController>();
    [SerializeField] private List<Vector3> storageCollectedPoses = new List<Vector3>();
    private Vector3 tempCollectedPos;
    [SerializeField] private TextMeshPro storageCounterText;
    [SerializeField] private StorageData storageData;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowStorageCounter();
    }
    public Vector3 GetDroppedAreaPos()
    {
        return storageDroppedArea.position;
    }
    public int GetStorageCubeCount()
    {
        return storageCubes.Count;
    }
    public void AddStorageCube(CubeController _cube)
    {
        storageCubes.Add(_cube);

        ShowStorageCounter();
    }
    public void DecreaseStorageCube(CubeController _cube)
    {
        storageCubes.Remove(_cube);

        ShowStorageCounter();
    }
    public bool IsOnStorage(CubeController _cube)
    {
        return storageCubes.Contains(_cube);
    }
    public Vector3 GetCubeTargetPos()
    {
        if (storageCollectedPoses.Count > 0)
        {
            tempCollectedPos = storageCollectedPoses[0];
            storageCollectedPoses.Remove(tempCollectedPos);

            return storageCubeParent.InverseTransformPoint(tempCollectedPos);
        }

        return new Vector3((((storageCubes.Count - 1) % 5)) * (-3.5f),
         0.0f,
         ((int)((storageCubes.Count - 1) / 5)) * (2.5f));
    }
    public void AddStorageCollectedPos(Vector3 _pos)
    {
        storageCollectedPoses.Add(_pos);
    }

    public void ShowStorageCounter()
    {
        storageCounterText.text = (storageCubes.Count) + " / " + storageData.storageCapacity;
    }
}
