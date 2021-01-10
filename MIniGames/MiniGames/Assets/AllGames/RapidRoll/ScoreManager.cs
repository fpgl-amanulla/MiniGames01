using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidRoll
{
    public class ScoreManager : MonoBehaviour
    {

        public static ScoreManager Instance;
        private int score = 0;
        private int bestScore = 0;

        private void Start()
        {
            if (Instance == null)
                Instance = this;

            bestScore = PlayerPrefs.GetInt("BestScore");
        }

        private void Update()
        {
            if (!GameManager.Instance.isGameStarted)
            {
                return;
            }
            if (GameManager.Instance.isGameOver)
            {
                if (score > bestScore)
                {
                    bestScore = score;
                }
                UIManager.Instance.UpdateGameOverPanel(score, bestScore);
                return;
            }
            score++;
            UIManager.Instance.UpDateScore(score);
        }
    }
}