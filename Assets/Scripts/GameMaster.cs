using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool runningTimer;
    public float currentTime;
    public float maxTime = 30;

    public PlayerLife player;
    public Finish finish;

    public Text timerText;

    void Start()
    {
        currentTime = maxTime;
        runningTimer = true;
    }

    public void Update()
    {
        if (runningTimer)
        {
            currentTime -= Time.deltaTime;
            timerText.text = "Death Timer: " + System.Math.Round(currentTime, 0);
            if (currentTime <= 0)
            {
                runningTimer = false;
                player.Die();
                StartCoroutine(RestartLevelAfterDelay(3));
            }
        }

    }

    IEnumerator RestartLevelAfterDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        RestartLevel();
    }

    public void StopTimer()
    {
        runningTimer = false;
    }

    public void RestartLevel()
    {
        currentTime = maxTime;
        runningTimer = true;
    }
}