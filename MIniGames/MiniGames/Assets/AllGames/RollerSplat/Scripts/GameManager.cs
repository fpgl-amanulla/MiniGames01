using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager singleton;

        public LevelCompleteTime levelCompleteTime;

        private GroundPiece[] allGoundPieces;

        public GameObject levelCompletePanel;
        public GameObject levelNotCompletePanel;
        public GameObject slider;
        public GameObject level;

        public bool levelClear = false;

        private void Start()
        {
            //GoogleAdManager.Instance.Display_Banner();
            //PlayerPrefs.SetInt("level", 0);

            levelCompletePanel = GameObject.Find("LevelComplete");
            levelNotCompletePanel = GameObject.Find("LevelNotComplete");
            slider = GameObject.Find("Slider");

            levelCompletePanel.SetActive(false);
            levelNotCompletePanel.SetActive(false);
            SetupNewLevel();
        }

        private void SetupNewLevel()
        {
            allGoundPieces = FindObjectsOfType<GroundPiece>();
        }

        private void Awake()
        {
            if (singleton == null)
                singleton = this;
            else if (singleton != this)
            {
                Destroy(gameObject);
            }
            //DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnlevelFinishedLoading;
        }

        private void OnlevelFinishedLoading(Scene arg0, LoadSceneMode arg1)
        {
            SetupNewLevel();
        }

        public void CheckComplete()
        {
            bool isFinished = true;
            for (int i = 0; i < allGoundPieces.Length; i++)
            {
                if (!allGoundPieces[i].isColored)
                {
                    isFinished = false;
                }
            }
            if (isFinished)
            {
                LevelComplete();
                //slider.SetActive(false);
                FindObjectOfType<Confetti_FX>().PlayConfetti();
                FindObjectOfType<Confetti_FX1>().PlayConfetti();
                //FindObjectOfType<LevelCompleteTime>().CancelInvoke();
            }
        }

        private void LevelComplete()
        {
            if (PlayerPrefs.GetInt("level") % 3 == 0 && PlayerPrefs.GetInt("level") > 1)
            {
                //GoogleAdManager.Instance.Display_InterstitialAd();
            }

            level.SetActive(false);
            StartCoroutine(Wait());
            StartCoroutine(NextLevel());
            levelClear = true;
            //Sc.instance.Fill();
            JumpAnim.instance.Jump();
            levelCompletePanel.SetActive(true);
            FindObjectOfType<Ball>().speed = 0;

            if (levelCompleteTime.timeCount <= 60.0f && levelCompleteTime.timeCount > 20.0f)
                StartCoroutine(Stars.Instance.GiveStar(2));
            else if (levelCompleteTime.timeCount <= 20.0f)
                StartCoroutine(Stars.Instance.GiveStar(3));
            else
                StartCoroutine(Stars.Instance.GiveStar(1));

            //FindObjectOfType<LevelCompleteTime>().isTimeEnd = true;
            //
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(.50f);
            //AdManager.instance.ShowVideoAd();
        }

        IEnumerator NextLevel()
        {
            yield return new WaitForSeconds(4.2f);
            int levelNo = PlayerPrefs.GetInt("level") + 1;
            PlayerPrefs.SetInt("level", levelNo);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject);
        }

        public void GameOver()
        {
            StartCoroutine(Wait());
            slider.SetActive(false);
            levelNotCompletePanel.SetActive(true);
            FindObjectOfType<Ball>().enabled = false;
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayAganin()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void NextLevelTemp()
        {
            int levelNo = PlayerPrefs.GetInt("level") + 1;
            PlayerPrefs.SetInt("level", levelNo);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}