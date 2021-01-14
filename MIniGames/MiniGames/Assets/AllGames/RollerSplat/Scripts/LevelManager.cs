using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelPrefabs levelPrefabs;

    void Start()
    {
        if (PlayerPrefs.GetInt("level") >= 99)
        {
            int levelIndex = Random.Range(5, 100);
            Instantiate(levelPrefabs.levelPrefabs[levelIndex]);
        }
        else
        {
            int level = PlayerPrefs.GetInt("level");
            GameObject newLevel = Instantiate(levelPrefabs.levelPrefabs[level]);
            //newLevel.AddComponent<MeshCombine>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
