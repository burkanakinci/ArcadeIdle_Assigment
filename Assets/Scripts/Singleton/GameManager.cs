using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }
    public event Action levelStart;
    private int level=0;

    private void Awake()
    {
        instance = this;

        PlayerPrefs.DeleteAll();
    }

    public void UnlockZones()
    {

    }

}
