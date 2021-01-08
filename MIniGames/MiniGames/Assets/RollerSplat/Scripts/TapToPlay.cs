using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour
{
    public void Play()
    {
        Ball.instance.isGameStarted = true;
        FindObjectOfType<LevelCompleteTime>().startCount = true;
        transform.gameObject.SetActive(false);
    }
}
