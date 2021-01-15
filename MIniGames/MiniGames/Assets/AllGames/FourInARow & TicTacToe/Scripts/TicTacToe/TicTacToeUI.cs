using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TicTacToeUI : MonoBehaviour
{
    public static TicTacToeUI Instance;
    public Button BtnBackToMain;
    public GameObject panelMenu;
    public Button btnPlay;

    public TextMeshProUGUI txtTurn;
    public TextMeshProUGUI txtWinner;

    public GameObject gameOverButtons;
    public List<Image> borderImages = new List<Image>();
    public List<Image> imgDirection = new List<Image>();

    private static int gameCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BtnBackToMain.onClick.AddListener(() => BackToMainCallBack());
        btnPlay.onClick.AddListener(() => PlayCallBack());
    }

    private void PlayCallBack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        GameManager.Instance.StartGame();
        Constant.SetActiveFalse(panelMenu);
        Constant.SetActiveTrue(txtTurn.gameObject);
        BoderAnimation();
    }
    public void BackToMainCallBack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);

        SceneManager.LoadScene("TicTakToe");

    }
    public void PlayAgainCallBack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        if (gameCount > 0 && (gameCount % 2) == 0)
            if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();

        GameManager.Instance.isGameStart = true;
        GameManager.Instance.isGameOver = false;

        txtTurn.gameObject.SetActive(true);
        txtWinner.gameObject.SetActive(false);
        TicTacToe.Instance.ResetGame();
        gameOverButtons.SetActive(false);
        BoderAnimation();
    }

    public void HomeCallBack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        //if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();
        SceneManager.LoadScene(0);
    }

    public void BoderAnimation()
    {
        foreach (var item in borderImages)
        {
            Vector3 _scale = new Vector3(item.rectTransform.localScale.x, 0, 0);
            item.rectTransform.localScale = _scale;
            item.rectTransform.DOScaleY(1f, 1.0f);
        }
    }

    internal void ResetDiretion()
    {
        for (int i = 0; i < imgDirection.Count; i++)
        {
            imgDirection[i].gameObject.SetActive(false);
        }
    }

    internal void GameOver()
    {
        gameCount++;
        gameOverButtons.SetActive(true);
        txtTurn.gameObject.SetActive(false);
        txtWinner.gameObject.SetActive(true);
        DisplayWinner();
        int winner = TicTacToe.previousWinState;
        if (winner == 1 || winner == 2)
        {
            GameOverAnimation();
            Handheld.Vibrate();
        }
    }

    public void GameOverAnimation()
    {
        string direction = TicTacToe.Instance.GetGameOverDirection().ToString();
        //Debug.Log(direction);
        for (int i = 0; i < imgDirection.Count; i++)
        {
            if (imgDirection[i].name == direction)
            {
                imgDirection[i].gameObject.SetActive(true);
                RectTransform rectTransform = imgDirection[i].rectTransform;
                rectTransform.localScale = new Vector3(0, 1, 1);
                rectTransform.DOScaleX(1.0f, 1.0f);

                return;
            }
        }
    }
    public void BoderAnimationReverse()
    {
        foreach (var item in borderImages)
        {
            Vector3 _scale = new Vector3(item.rectTransform.localScale.x, item.rectTransform.localScale.y, 0);
            item.rectTransform.localScale = _scale;
            item.rectTransform.DOScaleY(0f, 1.0f);
        }
    }

    private void DisplayWinner()
    {
        int winner = TicTacToe.previousWinState;
        string strWinText;
        switch (winner)
        {
            case 0:
                strWinText = "Draw";
                break;
            case 1:
                strWinText = "You Win";
                break;
            case 2:
                strWinText = "AI Win";
                break;
            default:
                strWinText = "Error.....";
                break;
        }
        txtWinner.text = strWinText;
    }

    internal void DisPlayTurn(int player)
    {
        if (player == 1)
            txtTurn.text = "Your Turn.Please Play";
        else if (player == 2)
            txtTurn.text = "Ai Turn.Please Wait";

    }
}
