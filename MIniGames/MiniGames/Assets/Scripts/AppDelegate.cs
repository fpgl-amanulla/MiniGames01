using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppDelegate
{
    public static AppDelegate Instance = null;

    #region SharedInstance
    public static AppDelegate SharedManager()
    {
        if (Instance == null) Instance = AppDelegate.Create();
        return Instance;
    }

    private static AppDelegate Create()
    {
        AppDelegate ret = new AppDelegate();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        return null;
    }

    private bool Init()
    {
        return true;
    }
    #endregion

    #region Settings
    public bool GetSoundStatus()
    {
        int value = PlayerPrefs.GetInt(Constant.SoundStatus, 1);
        return Convert.ToBoolean(value);
    }
    public void SetSoundStatus(bool _status)
    {
        if (_status)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
        PlayerPrefs.SetInt(Constant.SoundStatus, Convert.ToInt32(_status));
    }

    public bool GetVibrationStatus()
    {
        int value = PlayerPrefs.GetInt(Constant.VibrationStatus, 1);
        return Convert.ToBoolean(value);
    }
    public void SetVibrationStatus(bool _status) => PlayerPrefs.SetInt(Constant.VibrationStatus, Convert.ToInt32(_status));
    #endregion
}
