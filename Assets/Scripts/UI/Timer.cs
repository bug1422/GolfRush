using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI endingText;
    private static GameObject endingBoard;
    [SerializeField]
    [Tooltip("In second")]
    private float timeRemaining;
    [SerializeField]
    [Tooltip("In second")]
    private float timerOffset;
    private float timeCount;
    void Start()
    {
        endingBoard = transform.Find("EndingBoard").gameObject;
        print(endingBoard.GetInstanceID());
        endingBoard.SetActive(false);
        EventHandler.gameEnding += ShowEndingBoard;
        timerText.enabled = false;
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(timerOffset);
        var minute = Mathf.FloorToInt(timeRemaining / 60);
        var second = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minute, second);
        timerText.enabled = true;
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        timeCount = timeRemaining;
        while(Mathf.FloorToInt(timeCount) >= 0f)
        {
            SetTimer();
            yield return null;
        }
        timerText.enabled = false;
        EventHandler.GameEnding();
    }

    private void SetTimer()
    {
        timeCount -= Time.deltaTime;
        var minute = Mathf.FloorToInt(timeCount / 60);
        var second = Mathf.FloorToInt(timeCount % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minute, second);
    }

    private void ShowEndingBoard()
    {
        var text = endingText.text;
        text = text.Replace("<finalScore>", EventHandler.GetScore().ToString());
        endingText.text = text;
        print(endingBoard.GetInstanceID());
        endingBoard.SetActive(true);
    }

}
