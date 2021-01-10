using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance;
    public GameObject playerPrefabs;

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnPlayer()
    {
        StartCoroutine(SpawnWait());
    }
    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(playerPrefabs, this.transform.position, transform.rotation);
    }
}
