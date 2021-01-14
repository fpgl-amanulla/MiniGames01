using System;
using System.Collections;
using System.Collections.Generic;
using FIAR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverButtons : MonoBehaviour
{
    public static GameOverButtons Instance;

    [SerializeField] RectTransform panelGameoverBts;

    public Button btnPlayAgain;
    public Button btnHome;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        panelGameoverBts = this.gameObject.GetComponent<RectTransform>();

        panelGameoverBts.DOAnchorPosY(0, 1.0f).SetEase(Ease.OutBounce);

        btnPlayAgain.onClick.AddListener(() => PlayAgainCallback());
        btnHome.onClick.AddListener(() => HomeCallback());
    }

    private void HomeCallback()
    {
        if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    private void PlayAgainCallback()
    {
        if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();
        UiManager.Instance.InvokeAction(1);
        Destroy(this.gameObject);
    }

}
