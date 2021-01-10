using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance = null;

    public SO_Prefabs soPrefabs;

    public static MasterManager GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = GameObject.FindObjectOfType<MasterManager>(); ;
            return Instance;
        }
    }
}
