using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public int index;
    public int number = -2;
    public Button btnUpper;
    public Button btnLower;

    public TextMeshProUGUI txtNum;
    void Start()
    {
        btnUpper.onClick.AddListener(() => BtnUpperCallBack());
        btnLower.onClick.AddListener(() => BtnLowerCallBack());
    }

    private void BtnLowerCallBack()
    {
        if (number == -2 || number <= 0)
            number = 10;
        number--;
        txtNum.text = number.ToString();

        MainGameContoller.Instance.InputAction();
    }

    private void BtnUpperCallBack()
    {

        if (number == -2 || number >= 9)
        {
            number = -1;
        }
        number++;
        txtNum.text = number.ToString();

        MainGameContoller.Instance.InputAction();
    }

}
