using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    private Button btn;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.transform.DOScale(.9f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }
}
