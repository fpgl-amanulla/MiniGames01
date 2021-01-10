using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class ColorPickerDrawer : MonoBehaviour
{
    public static UnityAction action;
    [SerializeField] Button btnOpenColorDrawer;
    [SerializeField] Button btnCloseColorDrawer;
    [SerializeField] Button btnCloseColorDrawerL;
    public List<Button> colorBtns = new List<Button>();

    [SerializeField] RectTransform colorDrawerHolder;

    private void Start()
    {
        btnCloseColorDrawerL.gameObject.SetActive(false);
        btnOpenColorDrawer.onClick.AddListener(() => OpenColorDrawerCallBack());
        btnCloseColorDrawer.onClick.AddListener(() => CloseColorDrawerCallBack());
        btnCloseColorDrawerL.onClick.AddListener(() => CloseColorDrawerCallBack());

        for (int i = 0; i < colorBtns.Count; i++)
        {
            Color32 color = colorBtns[i].GetComponent<Image>().color;
            colorBtns[i].onClick.AddListener(() => SelectColorCallBack(color));

        }
    }

    private void OpenColorDrawerCallBack()
    {
        btnOpenColorDrawer.gameObject.SetActive(false);
        btnCloseColorDrawerL.gameObject.SetActive(true);
        colorDrawerHolder.DOAnchorPosY(0, 1.0f).SetEase(Ease.OutBounce);
    }

    private void CloseColorDrawerCallBack()
    {
        btnOpenColorDrawer.gameObject.SetActive(true);
        btnCloseColorDrawerL.gameObject.SetActive(false);
        colorDrawerHolder.DOAnchorPosY(-500, 1.0f).SetEase(Ease.OutBack);
    }


    private void SelectColorCallBack(Color32 _color)
    {
        PlayerPrefs.SetString("SelectedColor", ColorUtility.ToHtmlStringRGB(_color));
        action?.Invoke();
    }
}
