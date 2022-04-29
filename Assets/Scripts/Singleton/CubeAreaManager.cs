using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeAreaManager : MonoBehaviour

{

    private static CubeAreaManager instance;
    public static CubeAreaManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CubeAreaManager").AddComponent<CubeAreaManager>();
            }
            return instance;
        }
    }

    [SerializeField] private CubeAreaController[] cubeAreas;


    private void Awake()
    {
        instance = this;
    }

    public CubeAreaController UnlockCubeAreas(ZoneController _zone)
    {
        for (int i = cubeAreas.Length - 1; i >= 0; i--)
        {
            if ((_zone.GetZoneId()-1) ==(cubeAreas[i].GetCubeAreaId()))

            {
                cubeAreas[i].SetCubeAreaIsLock(false);
                cubeAreas[i].FillCubeArea();
                return cubeAreas[i];
            }
        }
        return null;
    }
    public void DecreaseCubeOnCubeArea(CubeController _cube)
    {
        
        for (int i = cubeAreas.Length - 1; i >= 0; i--)
        {
            if (cubeAreas[i].GetCubes().Contains(_cube))
            {
                cubeAreas[i].GetCubes().Remove(_cube);
                break;
            }
        }
    }
}
