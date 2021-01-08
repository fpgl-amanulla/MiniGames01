using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNo : MonoBehaviour
{
    private TextMeshProUGUI levelNoText;

    private void Start()
    {
        levelNoText = GetComponent<TextMeshProUGUI>();
        //levelNoText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        levelNoText.text = "Level " + (PlayerPrefs.GetInt("level") + 1).ToString();
    }
}
