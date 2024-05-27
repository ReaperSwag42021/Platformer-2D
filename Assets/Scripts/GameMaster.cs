using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool runningTimer;
    public float currentTime;
    public float maxTime = 30;

    public PlayerLife player;

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
            timerText.text = "Death Timer: " + currentTime;
            if (currentTime <= 0)
            {
                runningTimer = false;
                player.Die();
                RestartLevel();
            }
        }
            
    }

    public void RestartLevel()
    {
        currentTime = maxTime;
        runningTimer = true;
    }
}
