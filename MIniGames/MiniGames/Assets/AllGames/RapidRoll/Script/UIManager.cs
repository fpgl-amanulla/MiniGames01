using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace RapidRoll
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject gameOverPanel;

        public GameObject tapToPlayButton;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverScoreText;
        public TextMeshProUGUI bestScoreText;

        public ParticleSystem deathEffect;

        public int health = 3;

        private void Awake()
        {
            healthText.text = health.ToString();
            if (Instance == null)
                Instance = this;
        }

        public void Play()
        {
            tapToPlayButton.SetActive(false);
        }
        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void DeathEffect(Transform t)
        {
            Instantiate(deathEffect, t.position, t.rotation);
        }

        public void IncreaseHealth()
        {
            health++;
            healthText.text = health.ToString();
        }
        public void DecreaseHealth()
        {
            health--;
            healthText.text = health.ToString();


        }

        public void GameOverPanel()
        {
            healthText.text = health.ToString();
            gameOverPanel.SetActive(true);
        }
        public void UpdateGameOverPanel(int score, int bestScore)
        {
            gameOverScoreText.text = "Your Score : " + score.ToString();
            bestScoreText.text = "Best Score : " + bestScore.ToString();
        }
        public void UpDateScore(int score)
        {
            scoreText.text = score.ToString();
        }

    }
}