using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSound;
    [SerializeField] private Animator myAnimationController;
    public PlayerLife playerLife;

    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        myAnimationController = GetComponent<Animator>();
    }
    public void OpenChestAndCompleteLevel()
    {
        if (!levelCompleted)
        {
            finishSound.Play();
            myAnimationController.SetTrigger("PlayAnimChest");
            levelCompleted = true;
            playerLife.StopAllCoroutinesInPlayerLife(); // Stop all coroutines in PlayerLife, including the death timer
            Invoke("CompleteLevel", 8f);
        }
    }
}