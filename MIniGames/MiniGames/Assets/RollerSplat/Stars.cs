using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public static Stars Instance;

    public List<GameObject> stars;
    public GameObject starBg;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }



    private void Start()
    {
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
    }

    public IEnumerator GiveStar(int num)
    {
        starBg.SetActive(true);
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(.5f);
            stars[i].SetActive(true);
        }
    }
}
