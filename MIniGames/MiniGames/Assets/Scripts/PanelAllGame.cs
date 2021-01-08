using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelAllGame : MonoBehaviour
{
    public static PanelAllGame Instance = null;

    [Header("All Buttons for Games")]
    public List<Button> allGameButtons = new List<Button>();

    [Header("Transform of Gameobjects")]
    [SerializeField] Transform content;

    [Header("GameObjects")]
    [SerializeField] GameObject cameraMain;
    [SerializeField] GameObject panelAllGame;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Button btn = content.GetChild(i).GetComponent<Button>();
            allGameButtons.Add(btn);
        }

        for (int i = 0; i < allGameButtons.Count; i++)
        {
            var index = i;
            allGameButtons[i].onClick.AddListener(() => ButtonCallBack(index));
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GoogleAdManager.Instance.ShowInterestitialAD();
        }
    }

    private void ButtonCallBack(int index)
    {
        AudioManager.Instance.PlaySFX(AudioManager.audios.btnClick);
        string sceneName = Base.Constants.GetSceneName(index + 1);
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneName);
    }
}
