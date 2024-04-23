using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OpenChestAnimation()
    {
        if (!levelCompleted)
        {
            finishSound.Play();
            myAnimationController.SetTrigger("PlayAnimChest");
            levelCompleted = true;
        }
    }
}