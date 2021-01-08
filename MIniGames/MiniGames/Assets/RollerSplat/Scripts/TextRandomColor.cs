using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextRandomColor : MonoBehaviour
{
    public List<TextMeshProUGUI> text;

    private void Start()
    {
        for (int i = 0; i < text.Count; i++)
        {
            text[i].color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        }
    }
}
