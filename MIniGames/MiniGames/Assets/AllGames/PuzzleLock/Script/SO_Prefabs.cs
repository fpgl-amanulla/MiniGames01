using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Prefabs", menuName = "Create SO_Prefabs")]
public class SO_Prefabs : ScriptableObject
{
    public GameObject panelComplete;


    public GameObject LoadUIprefab(GameObject objectName, Transform parent)
    {
        return Instantiate(objectName, parent);
    }
}
