using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelSettings : MonoBehaviour
{
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnCross;
    [SerializeField] Toggle toggleSound;
    [SerializeField] Toggle toggleVibration;

    [SerializeField] GameObject panelSetting;
    [SerializeField] RectTransform settingsBG;

    private void Start()
    {
        panelSetting.gameObject.SetActive(false);
        btnSetting.onClick.AddListener(() =>
        {
            panelSetting.gameObject.SetActive(true);
            settingsBG.localScale = Vector2.one * .5f;
            settingsBG.DOScale(Vector3.one, .5f).SetEase(Ease.OutBounce);

            toggleSound.isOn = AppDelegate.SharedManager().GetSoundStatus();
            toggleVibration.isOn = AppDelegate.SharedManager().GetVibrationStatus();

            AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        });
        btnCross.onClick.AddListener(() =>
        {
            panelSetting.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        });

        toggleSound.onValueChanged.AddListener(delegate { ToggleSoundCallBack(toggleSound); });
        toggleVibration.onValueChanged.AddListener(delegate { ToggleVibrationCallBack(toggleSound); });
    }

    private void ToggleSoundCallBack(Toggle toggleSound) => AppDelegate.SharedManager().SetSoundStatus(toggleSound.isOn);

    private void ToggleVibrationCallBack(Toggle toggleVibration) => AppDelegate.SharedManager().SetSoundStatus(toggleVibration.isOn);
}
