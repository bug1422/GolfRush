using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    private static List<int> scoreboard = new List<int>() { 0 * 10 };
    private static int score;
    public static event Action gameEnding;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    static void UpdateScoreBoard()
    {
    }
    public static int GetScore()
    {
        return score;
    }

    public static void UpdateScore()
    {
        score += 1;
        print(score);
    }

    public static void GameEnding()
    {
        print("end");
        gameEnding?.Invoke();
        UpdateScoreBoard();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
