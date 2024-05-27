using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSound;
    [SerializeField] private Animator myAnimationController;

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
            Invoke("CompleteLevel", 8f);
        }
    }
}