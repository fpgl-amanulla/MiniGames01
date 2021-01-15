using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidRoll
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool isGameStarted = false;
        public bool isGameOver = false;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void StartPlay()
        {
            UIManager.Instance.Play();
            isGameStarted = true;
            AudioManager.Instance.PlayAudio();
        }

        public void GameOver()
        {
            isGameOver = true;
            UIManager.Instance.GameOverPanel();
            AudioManager.Instance.StopAudio();
        }
        public void PlayAgain()
        {
            if (GoogleAdManager.Instance != null) GoogleAdManager.Instance.ShowInterestitialAD();
            UIManager.Instance.PlayAgain();
        }
    }
}
