using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelCompleted : MonoBehaviour
{
    public Button btnNext;

    private void Start()
    {
        btnNext.onClick.AddListener(() => NextCallBack());
    }

    private void NextCallBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
