using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RS;

public class LevelCompleteTime : MonoBehaviour
{
    public float timeCount = 0;

    public bool startCount = false;

    private void Update()
    {
        if (RS.GameManager.singleton.levelClear)
            startCount = false;
        if(startCount)
            timeCount += Time.deltaTime;

    }
}
