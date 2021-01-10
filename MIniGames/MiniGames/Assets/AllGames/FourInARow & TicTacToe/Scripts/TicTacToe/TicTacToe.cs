using System;
using System.Collections;
using System.Collections.Generic;
using FIAR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;

public enum GameOverDirection
{
    VerticalMiddle,
    VerticalUp,
    VerticalDown,
    HorizontalMiddle,
    HorizontalUp,
    HorizontalDown,
    RightDiagonal,
    LeftDiagonal
}

public class TicTacToe : MonoBehaviour
{
    public static TicTacToe Instance;

    public List<Button> Cells;
    public Button[,] GameBoard = new Button[3, 3];
    public int[,] Board = new int[3, 3];

    public Sprite transpanrent;
    public Sprite O;
    public Sprite X;
    readonly int length = 3;
    readonly int human = 1;
    readonly int ai = 2;
    readonly int empty = 0;
    int player = -1;

    public static int previousWinState = 1;

    int difficulty = 10;

    private void Start()
    {
        Instance = this;


        int cellindex = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameBoard[i, j] = Cells[cellindex++];
                Board[i, j] = empty;
            }
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                var x = i;
                var y = j;
                GameBoard[i, j].onClick.AddListener(() => ButtonCallBack(x, y));
            }
        }

        SetPlayer(human);
        //StartCoroutine(AiTurn());
    }

    public void ResetGame()
    {
        difficulty = Random.Range(5, 11);

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameBoard[i, j].image.sprite = transpanrent;
                Board[i, j] = 0;
            }
        }
        TicTacToeUI.Instance.ResetDiretion();
        StartCoroutine(SetTurn());
    }

    private IEnumerator SetTurn()
    {
        yield return new WaitForSeconds(1.0f);
        if (previousWinState == 0)
        {
            int player = UnityEngine.Random.Range(1, 3);
            SetPlayer(player);
            if (player == 1)
            {

            }
            else
            {
                StartCoroutine(AiTurn());
            }
        }
        else
        {
            if (previousWinState == 1)
            {
                StartCoroutine(AiTurn());
                player = ai;
            }
            else
            {
                player = human;
            }
            SetPlayer(player);
        }
    }

    private void SetPlayer(int _player)
    {
        player = _player;

        TicTacToeUI.Instance.DisPlayTurn(player);
    }

    private void ButtonCallBack(int i, int j)
    {
        if (!GameManager.Instance.isGameStart || GameManager.Instance.isGameOver)
            return;
        if (player == human)
        {
            if (Board[i, j] == empty)
            {
                Board[i, j] = human;
                FillAnimation(i, j, O);
                if (EvaluateScore(Board) == -10)
                {
                    //GameOver
                    previousWinState = 1;
                    GameManager.Instance.GameOver();
                    TicTacToeUI.Instance.GameOver();
                    return;
                }
                if (IsMoveLeft(Board))
                {
                    SetPlayer(ai);
                    StartCoroutine(AiTurn());
                }
                else
                {
                    //Draw
                    previousWinState = 0;
                    GameManager.Instance.GameOver();
                    TicTacToeUI.Instance.GameOver();
                }
            }
        }
    }

    private void FillAnimation(int i, int j, Sprite _sprite)
    {
        GameBoard[i, j].image.sprite = _sprite;
        Image img = GameBoard[i, j].image;
        img.type = Image.Type.Filled;
        img.fillAmount = 0;
        DOTween.To(() => img.fillAmount, x => img.fillAmount = x, 1.0f, .3f);
    }

    private IEnumerator AiTurn()
    {
        yield return new WaitForSeconds(0.3f);
        var move = FindBestMove(Board);
        Board[move.Item1, move.Item2] = ai;
        FillAnimation(move.Item1, move.Item2, X);

        if (EvaluateScore(Board) == +10)
        {
            previousWinState = 2;
            TicTacToeUI.Instance.GameOver();
            GameManager.Instance.GameOver();
            yield break;
        }
        if (IsMoveLeft(Board))
        {
            SetPlayer(human);
        }
        else
        {
            previousWinState = 0;
            TicTacToeUI.Instance.GameOver();
            GameManager.Instance.GameOver();
        }
    }

    public bool IsMoveLeft(int[,] Board)
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (Board[i, j] == empty)
                    return true;
            }
        }
        return false;
    }
    public int EvaluateScore(int[,] Board)
    {
        for (int row = 0; row < length; row++)
        {
            if (Board[row, 0] == Board[row, 1] && Board[row, 1] == Board[row, 2])
            {
                if (Board[row, 0] == human)
                    return -10;
                else if (Board[row, 0] == ai)
                    return +10;
            }
        }
        for (int col = 0; col < length; col++)
        {
            if (Board[0, col] == Board[1, col] && Board[1, col] == Board[2, col])
            {
                if (Board[0, col] == human)
                    return -10;
                else if (Board[0, col] == ai)
                    return +10;
            }
        }
        if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
        {
            if (Board[0, 0] == human)
                return -10;
            if (Board[0, 0] == ai)
                return +10;
        }
        if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
        {
            if (Board[0, 2] == human)
                return -10;
            if (Board[0, 2] == ai)
                return +10;
        }

        return 0;
    }

    public GameOverDirection GetGameOverDirection()
    {
        GameOverDirection gameOverDirection = default;

        Vector2[] position = new Vector2[3];
        for (int row = 0; row < length; row++)
        {
            if (Board[row, 0] == Board[row, 1] && Board[row, 1] == Board[row, 2])
            {
                if (Board[row, 0] == human || Board[row, 0] == ai)
                {
                    if (row == 0) gameOverDirection = GameOverDirection.HorizontalUp;
                    if (row == 1) gameOverDirection = GameOverDirection.HorizontalMiddle;
                    if (row == 2) gameOverDirection = GameOverDirection.HorizontalDown;
                    
                }
            }
        }
        for (int col = 0; col < length; col++)
        {
            if (Board[0, col] == Board[1, col] && Board[1, col] == Board[2, col])
            {
                if (Board[0, col] == human || Board[0, col] == ai)
                {
                    if (col == 0) gameOverDirection = GameOverDirection.VerticalUp;
                    if (col == 1) gameOverDirection = GameOverDirection.VerticalMiddle;
                    if (col == 2) gameOverDirection = GameOverDirection.VerticalDown;
                }
            }
        }
        if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
        {
            if (Board[0, 0] == human || Board[0, 0] == ai)
            {
                gameOverDirection = GameOverDirection.RightDiagonal;
            }
        }
        if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
        {
            if (Board[0, 2] == human || Board[0, 2] == ai)
            {
                gameOverDirection = GameOverDirection.LeftDiagonal;
            }
        }

        return gameOverDirection;
    }

    public int MiniMax(int[,] Board, int depth, bool isMax)
    {
        int score = EvaluateScore(Board);

        if (score == +10 || score == -10 || IsMoveLeft(Board) == false || depth == 0)
            return score;

        if (isMax)
        {
            int bestScore = -10000000;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (Board[i, j] == empty)
                    {
                        Board[i, j] = ai;
                        int newScore = MiniMax(Board, depth - 1, false);
                        Board[i, j] = empty;
                        bestScore = Math.Max(newScore, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 10000000;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (Board[i, j] == empty)
                    {
                        Board[i, j] = human;
                        int newScore = MiniMax(Board, depth - 1, true);
                        Board[i, j] = empty;
                        bestScore = Math.Min(newScore, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    public (int, int) FindBestMove(int[,] Board)
    {
        int bestValue = -10000000;
        int row = -1;
        int col = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Board[i, j] == empty)
                {
                    Board[i, j] = ai;
                    int value = MiniMax(Board, difficulty, false);
                    Board[i, j] = empty;
                    if (value > bestValue)
                    {
                        bestValue = value;
                        row = i;
                        col = j;
                    }
                }
            }
        }
        return (row, col);
    }
}
