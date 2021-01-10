using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRandomColor : MonoBehaviour
{
    public Material[] mat;

    public Material wallMat;

    private void Start()
    {
        int index = Random.Range(0, mat.Length);
        wallMat.color = mat[index].color;
    }
}
