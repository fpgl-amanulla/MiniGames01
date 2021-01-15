using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RapidRoll
{
    public class PlayerController : MonoBehaviour
    {

        private CharacterController controller;
        private Vector3 moveDirection;
        private float inputDirection;
        public float verticalVelocity = 0;
        private float gravity = 10.0f;
        private float speed = 5.0f;
        public bool gameEnd = false;
        public GameObject lifeEffect;


        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (!GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver)
                return;

            moveDirection = Vector3.zero;
            inputDirection = Input.GetAxis("Horizontal") * speed;

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    inputDirection = 3.5f;
                }
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    inputDirection = -3.5f;
                }
            }

            if (!controller.isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
                moveDirection.x = inputDirection;
                moveDirection.y = verticalVelocity;
                controller.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                verticalVelocity = 0;
                moveDirection.x = inputDirection;
                moveDirection.y = verticalVelocity;
                controller.Move(moveDirection * Time.deltaTime);
            }


        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Dead"))
            {
                if(AppDelegate.SharedManager().GetVibrationStatus())
                {
                    Handheld.Vibrate();
                }

                if (UIManager.Instance.health <= 1)
                {
                    GameManager.Instance.GameOver();
                }
                else
                {
                    PlayerSpawn.Instance.SpawnPlayer();
                }
                UIManager.Instance.DecreaseHealth();
                UIManager.Instance.DeathEffect(transform);
                Destroy(gameObject);

            }
            if (other.CompareTag("life"))
            {
                if (UIManager.Instance.health < 5)
                {
                    UIManager.Instance.IncreaseHealth();
                    GameObject newLifeEffect = Instantiate(lifeEffect, other.transform.position, transform.rotation);
                    Destroy(newLifeEffect, 1.0f);
                }
                //Debug.Log(health);
                Destroy(other.gameObject);
            }
        }

    }
}