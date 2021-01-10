using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RapidRoll
{
    public class ButtonManager : MonoBehaviour
    {


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

    }
}