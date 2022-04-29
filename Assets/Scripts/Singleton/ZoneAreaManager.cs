using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAreaManager : MonoBehaviour

{

    private static ZoneAreaManager instance;
    public static ZoneAreaManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ZoneAreaManager").AddComponent<ZoneAreaManager>();
            }
            return instance;
        }
    }

    [SerializeField]private ZoneController[] zones;


    private void Awake()
    {
        instance = this;
    }

    public void UnlockZones()
    {
        for(int i=zones.Length-1;i>=0;i--)
        {
            if(!zones[i].GetIsLockZone())
            {
                continue;
            }

            zones[i].CanDropped();
        }
    }
}
