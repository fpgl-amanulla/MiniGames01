using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailedPanel : MonoBehaviour
{
    public Button addPlay;
    public Button tryAgain;

    void Start()
    {
        addPlay.onClick.AddListener(() => Btn_AddPlayCallBack());
        tryAgain.onClick.AddListener(() => Btn_TryAgainCallBack());
    }

    private void Btn_TryAgainCallBack()
    {
        Debug.Log("Restart Game");
        if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void Btn_AddPlayCallBack()
    {
        MainGameContoller.Instance.CountNumer += 1;
        MainGameContoller.Instance.ShoowRemainingAttempts();
        this.gameObject.SetActive(false);
        Debug.Log("Play Add");
    }
}
