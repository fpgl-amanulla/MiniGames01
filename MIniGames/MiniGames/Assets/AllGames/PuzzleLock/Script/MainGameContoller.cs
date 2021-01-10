using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using DG.Tweening;

public class MainGameContoller : MonoBehaviour
{
    public static MainGameContoller Instance;

    public Button btnSubmit;

    [SerializeField] private string pinNumber = "1234";

    public TextMeshProUGUI txtHint;
    public TextMeshProUGUI txtPreviousAttempt;
    public int CountNumer = 5;

    private List<InputHandler> inputHandlderList = new List<InputHandler>();

    public InputHandler cellPrefabs;
    public HorizontalCard horizontalCardPrefab;
    public Transform cellContainer;
    public GameObject scorePrefabs;
    public Transform scrollViewContent;
    public Transform scrollView;

    public GameObject panelValidInvalid;
    public GameObject faildpanel;

    int cardCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        faildpanel.SetActive(false);
    }

    public void InputAction()
    {
        if (IsReadyToSubmit())
        {
            if (!btnSubmit.gameObject.activeInHierarchy)
            {
                btnSubmit.gameObject.SetActive(true);
                //AnimateScale(btnSubmit);
            }
        }
    }

    private void Start()
    {
        SetPinNumber();
        btnSubmit.gameObject.SetActive(false);
        txtPreviousAttempt.gameObject.SetActive(false);
        panelValidInvalid.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            InputHandler inputHandler = Instantiate(cellPrefabs, cellContainer);
            inputHandlderList.Add(inputHandler);
        }

        btnSubmit.onClick.AddListener(() => BtnSubmitCallBack());
    }

    /*  public void AnimateScale(Button btn)
      {
          DOTween.Sequence().Append(btn.transform.DOScale(Vector3.one * .8f, 1.0f).SetEase(Ease.InOutElastic))
    .Append(btn.transform.DOScale(Vector3.one, 1.0f).SetEase(Ease.InOutElastic)).SetLoops(-1);
      }*/


    private void BtnSubmitCallBack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);

        DOTween.Sequence().Append(txtHint.DOFade(0, 1.0f)).Append(txtHint.DOFade(1, 1.0f)).SetLoops(-1);
        if (!IsReadyToSubmit())
        {
            //txtHint.text = "Plese fill all field";
            return;
        }

        if (!txtPreviousAttempt.gameObject.activeSelf)
        {
            txtPreviousAttempt.gameObject.SetActive(true);
            panelValidInvalid.SetActive(true);
        }

        List<InputIfo> previousInputInfo = new List<InputIfo>();

        for (int i = 0; i < inputHandlderList.Count; i++)
        {
            Color colorCode;

            if (inputHandlderList[i].number == (int)pinNumber[i] - 48)
            {
                inputHandlderList[i].txtNum.color = Color.green;
                colorCode = Color.green;
            }
            else
            {
                //inputHandlderList[i].txtNum.color = Color.red;
                colorCode = Color.red;

                for (int j = 0; j < pinNumber.Length; j++)
                {
                    if (inputHandlderList[i].number == (int)pinNumber[j] - 48)
                    {
                        // inputHandlderList[i].txtNum.color = Color.yellow;
                        colorCode = Color.yellow;
                        break;
                    }

                }
            }
            InputIfo inputInfo = new InputIfo(inputHandlderList[i].number, colorCode);
            previousInputInfo.Add(inputInfo);
        }
        cardCount++;
        if (cardCount > 2)
        {
            txtPreviousAttempt.transform.SetParent(scrollView.transform);
            txtPreviousAttempt.rectTransform.anchoredPosition = new Vector2(320, 115);
        }
        HorizontalCard card = Instantiate(horizontalCardPrefab, scrollViewContent);
        card.Init(previousInputInfo);

        CountNumer--;
        if (IsCompleted())
        {
            SO_Prefabs soPrefabs = MasterManager.GetInstance.soPrefabs;
            soPrefabs.LoadUIprefab(soPrefabs.panelComplete, this.transform);
        }
        else if (CountNumer < 1)
        {
            faildpanel.SetActive(true);
        }
        ShoowRemainingAttempts();
    }
    public void ShoowRemainingAttempts() => txtHint.text = "Attempts Remaining: " + CountNumer;

    public bool IsCompleted()
    {
        for (int i = 0; i < inputHandlderList.Count; i++)
        {
            if (inputHandlderList[i].number != (pinNumber[i] - '0'))
                return false;
        }
        return true;
    }

    public void ResetInputHander()
    {
        for (int i = 0; i < inputHandlderList.Count; i++)
        {
            inputHandlderList[i].number = -2;
        }
    }

    public bool IsReadyToSubmit()
    {
        for (int i = 0; i < inputHandlderList.Count; i++)
        {
            if (inputHandlderList[i].number == -2)
                return false;
        }
        return true;
    }

    public void SetPinNumber() => pinNumber = GenerateUniquePin;

    public string GenerateUniquePin
    {
        get
        {
            List<int> uniqueNumber = new List<int>();
            string number = "";
            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    int value = Random.Range(0, 10);
                    if (!uniqueNumber.Contains(value))
                    {
                        uniqueNumber.Add(value);
                        number += value;
                        break;
                    }
                }
            }
            return number;
        }
    } 
}
