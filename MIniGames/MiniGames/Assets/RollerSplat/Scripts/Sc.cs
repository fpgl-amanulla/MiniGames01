using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc : MonoBehaviour
{

    public static Sc instance;

    public List<Vector3> groundPieces = new List<Vector3>();

    public GameObject prefabs;



    void Start()
    {
        if (instance == null)
            instance = this;

        int i = 0;
        while (i <= transform.childCount)
        {
            if (transform.GetChild(i).gameObject.CompareTag("GroundPiece"))
            {
                Vector3 t = gameObject.transform.GetChild(i).position;
                groundPieces.Add(t);
            }
            i++;
        }
    }


    public void Fill()
    {
       
        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {

        for(int i = 1; i < groundPieces.Count; i++)
        {
            yield return new WaitForSeconds(.2f);
            Instantiate(prefabs, groundPieces[i], Quaternion.identity);
        }
        

    }

}
