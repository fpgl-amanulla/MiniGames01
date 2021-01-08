using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeCallBack : MonoBehaviour
{
    private Button btnHome;

    private void Start()
    {
        btnHome = GetComponent<Button>();
        btnHome.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
            if (SceneLoader.Instance != null)
                SceneLoader.Instance.LoadScene(0);
            else
                SceneManager.LoadScene(0);
        });
    }


}
