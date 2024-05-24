using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool runningTimer;
    public float currentTime;
    public float maxTime = 40;

    public PlayerLife player;

    void Start()
    {
        currentTime = maxTime;
        runningTimer = true;
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            runningTimer = false;
            player.Die();
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        currentTime = maxTime;
        runningTimer = true;
    }
}
