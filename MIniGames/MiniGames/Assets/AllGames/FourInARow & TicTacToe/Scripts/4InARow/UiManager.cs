using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace FIAR
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance = null;

        public UnityAction<int, int> action;

        [Header("Panels")]
        public GameObject panelFourInARow;

        public TextMeshProUGUI txtWhoseTurn;
        public TextMeshProUGUI txtWinner;

        [Space(2)]
        [Header("Gameover Components")]
        public GameObject panelGameOverButtons;
        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        #region Private Variables
        private GameObject fourInARow;
        private GameObject gameOberButtons;

        #endregion
        int _tag;

        public void LoadPanel(int id)
        {
            switch (id)
            {
                case (int)Panel.FourInARow:
                    if (!fourInARow)
                        fourInARow = Instantiate(panelFourInARow, this.transform);
                    break;
                case (int)Panel.GameOverButtons:
                    if (!gameOberButtons)
                        gameOberButtons = Instantiate(panelGameOverButtons, this.transform);
                    break;

                default:
                    break;
            }
        }

        public void DisplayWhoseTurn(Player _player)
        {
            if (txtWhoseTurn.gameObject.activeSelf == false)
            {
                txtWhoseTurn.gameObject.SetActive(true);
            }
            if (_player == Player.Human)
            {
                txtWhoseTurn.color = Color.red;
                txtWhoseTurn.text = "Your turn.Plz play...";
            }
            else
            {
                txtWhoseTurn.color = Color.black;
                txtWhoseTurn.text = "AI's turn.Plz wait...";
            }
        }

        public void DisPlayWinner(Player _player)
        {
            txtWhoseTurn.gameObject.SetActive(false);
            txtWinner.gameObject.SetActive(true);
            if (_player == Player.Human)
            {
                txtWinner.text = "YOU WIN";
            }
            else if (_player == Player.Ai)
            {
                txtWinner.text = "AI WIN";
            }
            else
            {
                txtWinner.text = "Draw";
            }
        }

        public void GameOverMenu(int tag)
        {
            txtWhoseTurn.gameObject.SetActive(false);
            _tag = tag;
            LoadPanel((int)Panel.GameOverButtons);
        }
        public void InvokeAction(int index)
        {
            action.Invoke(_tag, index);
        }
    }
}
